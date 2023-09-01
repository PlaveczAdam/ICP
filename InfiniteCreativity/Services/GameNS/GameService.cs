using AutoMapper;
using Extensions;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.Enums.GameNS;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Services.CoreNS;
using InfiniteCreativity.Services.GameNS.EnemyGeneratorNS;
using Map;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

namespace InfiniteCreativity.Services.GameNS
{
    public class GameService : IGameService
    {
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;
        private IPlayerService _playerService;
        private IMapper _mapper;
        private EnemyGenerator _enemyGenerator;
        private Random _rnd = new Random();
        private IGameEndService _gameEndService;
        private INotificationService _notificationService;

        public GameService(InfiniteCreativityContext context, ICharacterService characterService, IPlayerService playerService, IMapper mapper, EnemyGenerator enemyGenerator, IGameEndService gameEndService, INotificationService notificationService)
        {
            _context = context;
            _characterService = characterService;
            _playerService = playerService;
            _mapper = mapper;
            _enemyGenerator = enemyGenerator;
            _gameEndService = gameEndService;
            _notificationService = notificationService;
        }

        public async Task EndGame()
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(withGConnections: true);
            await _gameEndService.Endgame(currentPlayer.GConnections.First().ConnectionID);
            await _context.SaveChangesAsync();
        }

       

        public async Task<ShowGameMapDTO> GetMap()
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(withGConnections: true);
            var map = _context.Map
                .Include(x=>x.HexTiles)
                .First(x => x.GConnection == currentPlayer.GConnections.First());
            var mapAccessor = new GameMapAccessor(map);

            return _mapper.Map<ShowGameMapDTO>(mapAccessor);
        }

        public async Task<ShowGameTurnDTO> GetTurn()
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail:true);
            var characters = gconn.Characters.OrderBy(x=>x.Order).ToList();
            var currInd = (gconn.Turn - 1) % gconn.Characters.Count();
            var gTurnDTO = new ShowGameTurnDTO() { 
                Turn = gconn.Turn,
                NextInTurnCharacterId = characters[currInd].Character.Id
            };
            return gTurnDTO;
        }
        private async Task<GConnection> GetGameConnectionDetailed(bool withGameCharacters = false, bool withCharacterDetail = false, bool withMap = false, bool withEnemy=false)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(withGConnections: true);
            var gconn = _context.GConnection.AsQueryable();
            if (withGameCharacters)
            {
                if (withCharacterDetail)
                {
                    gconn = gconn.Include(x => x.Characters).ThenInclude(x=>x.Character);
                }
                else
                {
                    gconn = gconn.Include(x => x.Characters);
                }
            }
            if (withMap)
            {
                gconn = gconn.Include(x => x.Map).ThenInclude(x=>x.HexTiles).ThenInclude(x=>x.Enemy);
            }
            if (withEnemy)
            {
                gconn = gconn.Include(x => x.Enemies);
            }
            return await gconn.SingleAsync(x => x.Id == currentPlayer.GConnections.First().Id);
        }

        public async Task<ShowGameTurnDTO> ProgressTurn()
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true);
            var characters = gconn.Characters.OrderBy(x => x.Order).ToList();
            gconn.Turn++;
            var currInd = (gconn.Turn - 1) % gconn.Characters.Count();
            var character = characters[currInd];
            character.Character.CurrentMovement = character.Character.Movement;
            var gTurnDTO = new ShowGameTurnDTO()
            {
                Turn = gconn.Turn,
                NextInTurnCharacterId = characters[currInd].Character.Id
            };
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(gconn.PlayerId);
            return gTurnDTO;
        }

        public async  Task StartGame(CreateGameDTO createGameDTO)
        {
            if (createGameDTO.CharacterIds.Count() > 3 || createGameDTO.CharacterIds.Count() < 0)
            {
                throw new ArgumentException();
            }
            var currentPlayer = await _playerService.GetCurrentPlayer(withGConnections:true);
            var oldMap = _context.Map.FirstOrDefault(x=>x.GConnection==currentPlayer.GConnections.First());
            if (oldMap is not null)
            { 
                _context.Remove(oldMap);
            }
            var characters = createGameDTO.CharacterIds.Select(async x => await _characterService.GetCharacterById(x, currentPlayer, withEquipment: true)).Select(x=>x.Result).ToList();
            MapGenerator generator = new();
            var map = generator.GenerateAndPlacePlayer(characters);
            map.GConnection = currentPlayer.GConnections.First();
            var ind = 0;
            characters.ForEach(x => _context.GameCharacter.Add(new GameCharacter()
            {
                Character = x,
                Order = ind++,
                Connection = map.GConnection
            })) ;
            characters.ForEach(x => x.CurrentHealth = x.Health);
            characters.ForEach(x => x.CurrentMovement = x.Movement);
            _context.Map.Add(map);
            await GenerateAndPlaceEnemys();
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(currentPlayer.Id);
        }

        public async Task<ShowWalkResultDTO> WalkPlayerRoute(CreatePlayerRouteDTO playerRoute)
        {
            if (playerRoute.Route.Count() < 1)
            { throw new InvalidOperationException(); }
            var walkRes = new ShowWalkResultDTO();

            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true, withMap:true);
            var characters = gconn.Characters.OrderBy(x => x.Order).ToList();
            var currInd = (gconn.Turn - 1) % gconn.Characters.Count();
            var character = characters[currInd];
            var ch = character.Character;

            var gma = new GameMapAccessor(gconn.Map);
            var walkedTiles = new List<HexTileDataObject>();

            var currentTile = gma.GetTileByIndex(ch.Row!.Value, ch.Col!.Value);
            var currentTileIsWater = currentTile!.IsWater();

            var remainingMovementPoints = ch.CurrentMovement;

            foreach (var tile in playerRoute.Route) {
                if (remainingMovementPoints < 1)
                { throw new InvalidOperationException(); }
                var tileData = gma.GetTileByIndex(tile.RowIndex, tile.ColIndex);
                if (tileData is null)
                { throw new InvalidOperationException(); }
                if (!tileData.TileContent.IsWalkable())
                { throw new InvalidOperationException(); }
                if(!currentTile.GetNeighbours().Contains(tileData))
                { throw new InvalidOperationException(); }
                var cost = tileData.IsWater() != currentTile.IsWater() ? ch.CurrentMovement : 1;
                remainingMovementPoints -= cost;
                currentTile = tileData;
                if (tileData.Enemy is not null)
                {
                    walkRes.InCombat = true;
                    ch.CurrentMovement = remainingMovementPoints;
                    ch.Col = tileData.ColIdx;
                    ch.Row = tileData.RowIdx;
                    walkRes.RemainingMovementPoints = ch.CurrentMovement;
                    walkRes.EndColumn = ch.Col.Value;
                    walkRes.EndRow = ch.Row.Value;
                    ch.IsInCombat = true;
                    await _context.SaveChangesAsync();

                    return walkRes;
                }
            }
            var lastTile = playerRoute.Route.Last();
            ch.CurrentMovement = remainingMovementPoints;
            ch.Col = lastTile.ColIndex;
            ch.Row = lastTile.RowIndex;
            walkRes.RemainingMovementPoints = ch.CurrentMovement;
            walkRes.EndColumn = ch.Col.Value;
            walkRes.EndRow = ch.Row.Value;
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(gconn.PlayerId);

            return walkRes;
        }
        private async Task GenerateAndPlaceEnemys()
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true, withEnemy:true);
            gconn.Enemies.Clear();
            var characters = gconn.Characters.OrderBy(x => x.Order).ToList();
            var level = characters.Average(x => x.Character.Level);
            var gma = new GameMapAccessor(gconn.Map);

            var emptyTiles = gma.HexTiles.SelectMany(hexTiles => hexTiles)
                .Where(hexTile => hexTile.TileContent.IsWalkable() && !characters.Any(y=>(y.Character.Col == hexTile.ColIdx && y.Character.Row == hexTile.RowIdx)))
                .ToList().ShuffleInPlace(_rnd);

            var enemyTiles = Enumerable.Range(0, (int)_rnd.NextDouble(emptyTiles.Count() * 0.1, emptyTiles.Count() * 0.5)).Select(x => emptyTiles[x]);
            enemyTiles.ForEach(x => { 
                x.Enemy = _enemyGenerator.Generate(level);
                x.Enemy.GConnection = gconn;
            });
        }
    }
}
