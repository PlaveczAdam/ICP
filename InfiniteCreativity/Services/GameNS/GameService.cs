using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Services.CoreNS;
using Map;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services.GameNS
{
    public class GameService : IGameService
    {
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;
        private IPlayerService _playerService;
        private IMapper _mapper;

        public GameService(InfiniteCreativityContext context, ICharacterService characterService, IPlayerService playerService, IMapper mapper)
        {
            _context = context;
            _characterService = characterService;
            _playerService = playerService;
            _mapper = mapper;
        }

        public async Task EndGame()
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(withGConnections: true);
            var gconn = _context.GConnection
                .Include(x=>x.Characters)
                .Include(x=>x.Map)
                .ThenInclude(x=>x.HexTiles)
                .ThenInclude(x=>x.DetailEntity)
                .FirstOrDefault(x=>x.ConnectionID==currentPlayer.GConnections.First().ConnectionID);
            _context.GConnection.Remove(gconn);
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

        public Task<ShowGameTurnDTO> GetTurn()
        {
            throw new NotImplementedException();
        }

        public Task<ShowGameTurnDTO> ProgressTurn()
        {
            throw new NotImplementedException();
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
            _context.Map.Add(map);
            await _context.SaveChangesAsync();
        }
    }
}
