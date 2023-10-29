using AutoMapper;
using DTOs.Enums.CoreNS;
using DTOs.Enums.GameNS;
using DTOs.Game;
using Extensions;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Extensions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.Enums.CoreNS;
using InfiniteCreativity.Models.Enums.GameNS;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Models.GameNS.Enemys;
using InfiniteCreativity.Services.CoreNS;
using InfiniteCreativity.Services.GameNS.EnemyGeneratorNS;
using InfiniteCreativity.Services.MapPatherNS;
using Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using static MoreLinq.Extensions.ForEachExtension;

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
        private MapPather _mapPather;
        private TurnSimulator _turnSimulator;
        private MapGeneratorPresets _mapGeneratorPresets;

        public GameService(InfiniteCreativityContext context,
            ICharacterService characterService,
            IPlayerService playerService,
            IMapper mapper,
            EnemyGenerator enemyGenerator,
            IGameEndService gameEndService,
            INotificationService notificationService,
            MapPather mapPather,
            TurnSimulator turnSimulator,
            MapGeneratorPresets mapGeneratorPresets)
        {
            _context = context;
            _characterService = characterService;
            _playerService = playerService;
            _mapper = mapper;
            _enemyGenerator = enemyGenerator;
            _gameEndService = gameEndService;
            _notificationService = notificationService;
            _mapPather = mapPather;
            _turnSimulator = turnSimulator;
            _mapGeneratorPresets = mapGeneratorPresets;
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
                .ThenInclude(x=>x.Enemy)
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
        private async Task<GConnection> GetGameConnectionDetailed(
            bool withGameCharacters = false,
            bool withCharacterDetail = false,
            bool withMap = false,
            bool withEnemy=false,
            bool withInventory=true,
            bool withBattle=false
            )
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
                gconn = gconn
                    .Include(x => x.Map)
                        .ThenInclude(x => x.HexTiles)
                            .ThenInclude(x => x.Enemy)
                    .Include(x => x.Map)
                        .ThenInclude(x => x.HexTiles)
                            .ThenInclude(x => x.EnemyBlueprint)
                                .AsSplitQuery();
            }
            if (withEnemy)
            {
                gconn = gconn.Include(x => x.Enemies).AsSplitQuery();
            }
            if (withInventory)
            {
                gconn = gconn
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.Head)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.Shoulder)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.Chest)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.Hand)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.Leg)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.Boot)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.Weapon)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.SkillSlots)
                                .ThenInclude(x => x.SkillHolder)
                                    .ThenInclude(x => x.Skill)
                                        .ThenInclude(x => x.Buffs)
                    .Include(x => x.Characters)
                        .ThenInclude(x => x.Character)
                            .ThenInclude(x => x.SkillSlots)
                                .ThenInclude(x => x.SkillHolder)
                                    .ThenInclude(x => x.Skill)
                                        .ThenInclude(x => x.Conditions)
                                            .AsSplitQuery();
            }
            if (withBattle)
            {
                gconn = gconn
                    .Include(x => x.Battle)
                        .ThenInclude(x => x.Participants)
                            .ThenInclude(x => x.Character)
                    .Include(x => x.Battle)
                        .ThenInclude(x => x.Participants)
                            .ThenInclude(x => x.Enemy)
                    .Include(x => x.Battle)
                        .ThenInclude(x => x.Participants)
                            .ThenInclude(x => x.Buffs)
                    .Include(x => x.Battle)
                        .ThenInclude(x => x.Participants)
                            .ThenInclude(x => x.Conditions)
                                .AsSplitQuery();
            }
            return await gconn.Where(x => x.Id == currentPlayer.GConnections.First().Id).SingleAsync();
        }
        
        public async Task<ShowGameTurnDTO> ProgressTurn()
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true, withInventory: true);
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
                await _gameEndService.Endgame(currentPlayer.GConnections.First().ConnectionID, removeGameObjectsOnly:true);
                await _context.SaveChangesAsync();
                currentPlayer = await _playerService.GetCurrentPlayer(withGConnections: true);
            }

            var characters = createGameDTO.CharacterIds.Select(async x => await _characterService.GetCharacterById(x, currentPlayer, withEquipment: true)).Select(x=>x.Result).ToList();
            MapGenerator generator = new();
            var map = generator.GenerateAndPlacePlayer(characters, _mapGeneratorPresets.GetPresetByMapType(createGameDTO.Maptype));
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
            characters.ForEach(x => x.IsInCombat = false);
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

            if (ch.IsInCombat)
            {
                throw new InvalidOperationException();
            }

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
                    CreateBattle(ch, tileData.Enemy, characters, gma, currentTile, gconn);
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

        private void CreateBattle(Character ch, Enemy enemy, List<GameCharacter> characters, GameMapAccessor gma, HexTileDataObject enemyTile, GConnection gconn)
        {
            var chars = new List<Character>() { ch };
            var mappedChars = characters.Select(x => x.Character);
            mappedChars
                .ForEach(x =>{
                    if (x == ch)
                    {
                        return;
                    }
                    var xTile = gma.GetTileByIndex(x.Row!.Value, x.Col!.Value);
                    var path = _mapPather.FindPathsUsingDijkstra(enemyTile, 3 , xTile!);
                    if (path is not null)
                    {
                        chars.Add(x);
                    }
                });

            var battle = new Battle() { Participants = new List<BattleParticipant>()};
            gconn.Battle = battle;
            var battleParticEnemy = new BattleParticipant();
            battleParticEnemy.Id = Guid.NewGuid();
            battleParticEnemy.Enemy = enemy;
            battleParticEnemy.CurrentSpeed = enemy.Speed;
            battle.Participants.Add(battleParticEnemy);
            

            var followUpEnemyNumber = _rnd.Next(0, chars.Count() + 1);
            var enemyList = new List<Enemy>();
            for (int i = 0; i < followUpEnemyNumber; i++)
            {
                enemyList.Add(_enemyGenerator.Generate(enemy.Level));
            }

            enemyList.ForEach(x =>{
                var battleParticEnemy = new BattleParticipant();
                battleParticEnemy.Id = Guid.NewGuid();
                battleParticEnemy.Enemy = x;
                battleParticEnemy.CurrentSpeed = x.Speed;
                x.GConnection = enemy.GConnection;
                battle.Participants.Add(battleParticEnemy);
            });

            chars.ForEach(x => {
                var battleParticipantChar = new BattleParticipant();
                battleParticipantChar.Id = Guid.NewGuid();
                battleParticipantChar.Character = x;
                battleParticipantChar.CurrentSpeed = x.Speed;
                x.CurrentAbilityResource = x.AbilityResource;
                x.CurrentHealth = x.Health;
                battle.Participants.Add(battleParticipantChar);
            });
            battle.Participants
                .OrderByDescending(x => x.CurrentSpeed)
                .Select((x,ind) => new { Ind = ind, Participant = x })
                .ForEach(x => x.Participant.Order = x.Ind);
            _context.Add(battle);
        }

        private async Task GenerateAndPlaceEnemys()
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true, withEnemy:true);
            gconn.Enemies.Clear();
            var characters = gconn.Characters.OrderBy(x => x.Order).ToList();
            var level = characters.Average(x => x.Character.Level);
            var gma = new GameMapAccessor(gconn.Map);

            var preDefinedEnemy = gma.HexTiles.SelectMany(x => x).Where(x => x.EnemyBlueprint is not null);
            preDefinedEnemy.ForEach(x => {
                x.Enemy = _enemyGenerator.Generate(level + 2, isBoss: x.EnemyBlueprint.IsBoss, type: x.EnemyBlueprint.Type);
                x.Enemy.GConnection = gconn;
            });

            var emptyTiles = gma.HexTiles.SelectMany(hexTiles => hexTiles)
                .Where(hexTile => hexTile.TileContent.IsSpawnable() && !characters.Any(y=>(y.Character.Col == hexTile.ColIdx && y.Character.Row == hexTile.RowIdx)) && hexTile.Enemy is null)
                .ToList().ShuffleInPlace(_rnd);

            var enemyTiles = Enumerable.Range(0, (int)_rnd.NextDouble(emptyTiles.Count() * 0.1, emptyTiles.Count() * 0.2)).Select(x => emptyTiles[x]);
            enemyTiles.ForEach(x => { 
                x.Enemy = _enemyGenerator.Generate(level);
                x.Enemy.GConnection = gconn;
            });
        }

        public async Task<ShowBattleStateDTO> StartBattle()
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true, withBattle: true, withMap:true);
            if (gconn is null)
            {
                throw new UnauthorizedAccessException();
            }
            if (gconn.Battle is null)
            {
                throw new InvalidOperationException();
            }
            if (gconn.Battle.HasStarted)
            {
                throw new InvalidOperationException();
            }
            var mapAccessor = new GameMapAccessor(gconn.Map);
            gconn.Battle.HasStarted = true;
            gconn.Characters.ForEach(x => x.Character.SkillSlots.ForEach(y => y.CurrentCooldown = 0));
            var enemyActions = HandleNpcTurn(gconn.Battle, mapAccessor);
            var battleState = _mapper.Map<ShowBattleStateDTO>(gconn.Battle);
            battleState.BattleEvents = enemyActions;
            battleState.TurnPredictions = _turnSimulator.Predict(gconn.Battle, 20);

            await _context.SaveChangesAsync();
            return battleState;
        }

        private List<ShowBattleEventDTO> HandleNpcTurn(Battle battle, GameMapAccessor mapAccessor)
        {
            var actions = new List<ShowBattleEventDTO>();
            var nextInTurn = _turnSimulator.GetNext(battle);
            var characterParticipants = battle.Participants.Where(x => x.Character is not null).ToList();
            var enemyParticipants = battle.Participants.Where(x => x.Enemy is not null).ToList();

            while (nextInTurn.Character is null)
            {
                actions.AddRange(TickBuffs(battle));
                actions.AddRange(TickConditions(battle));
                actions.Add(ProcessCooldown(battle));
                if (characterParticipants.All(x => !x.IsAlive))
                {
                    actions.Add(HandleDefeat(battle, mapAccessor));
                    return actions;
                }

                if (enemyParticipants.All(x => !x.IsAlive))
                {
                    actions.Add(HandleVictory(battle, mapAccessor));
                    return actions;
                }

                if (!nextInTurn.IsAlive)
                {
                    nextInTurn = _turnSimulator.GetNext(battle);
                    continue;
                }
  
                actions.Add(new ShowBattleEventNextInTurnDTO()
                {
                    SourceParticipantId = nextInTurn.Id,
                    TargetParticipantId = nextInTurn.Id
                });
                actions.AddRange(nextInTurn.Turn(battle.Participants.ToList()));

                nextInTurn = _turnSimulator.GetNext(battle);
            }

            actions.AddRange(TickBuffs(battle));
            actions.AddRange(TickConditions(battle));
            actions.Add(ProcessCooldown(battle));
            if (characterParticipants.All(x => !x.IsAlive))
            {
                actions.Add(HandleDefeat(battle, mapAccessor));
                return actions;
            }

            if (enemyParticipants.All(x => !x.IsAlive))
            {
                actions.Add(HandleVictory(battle, mapAccessor));
                return actions;
            }

            if (!nextInTurn.IsAlive)
            {    
                actions.AddRange(HandleNpcTurn(battle, mapAccessor));
            }

            actions.Add(new ShowBattleEventNextInTurnDTO()
            {
                SourceParticipantId = nextInTurn.Id,
                TargetParticipantId = nextInTurn.Id
            });

            return actions;
        }

        private ShowBattleEventDTO ProcessCooldown(Battle battle)
        {
            battle.Participants
                .Where(x => x.Character is not null)
                    .ForEach(x => x.Character.SkillSlots.ForEach(y => y.CurrentCooldown--));
            return new ShowBattleEventSkillCooldownDTO { };
        }

        private IEnumerable<ShowBattleEventDTO> TickBuffs(Battle battle)
        {
            var result = new List<ShowBattleEventDTO>();
            battle.Participants.ForEach(x =>
            {
                x.Buffs.OrderBy(y => y.BuffType).ForEach(y => result.Add(y.Tick(_mapper)));
                var expiredBuffs = x.Buffs.Where(y => y.Duration <= 0);
                expiredBuffs.ForEach(y => result.Add(new ShowBattleEventBuffExpiredDTO()
                {
                    SourceParticipantId = x.Id,
                    TargetParticipantId = x.Id,
                    Buff = _mapper.Map<ShowBuffDTO>(y),
                }));
                x.Buffs.RemoveAll(y => y.Duration <= 0);
            });
            return result;
        }

        private IEnumerable<ShowBattleEventDTO> TickConditions(Battle battle)
        {
            var result = new List<ShowBattleEventDTO>();
            battle.Participants.ForEach(x =>
            {
                foreach (var condition in x.Conditions.OrderBy(y => y.ConditionType))
                {
                    result.AddRange(condition.Tick(_mapper));
                    if (x.Conditions.Count == 0)
                    {
                        break;
                    }
                }
                var expiredConditions = x.Conditions.Where(y => y.Duration <= 0);
                expiredConditions.ForEach(y => result.Add(new ShowBattleEventConditionExpiredDTO()
                {
                    SourceParticipantId = x.Id,
                    TargetParticipantId = x.Id,
                    Condition = _mapper.Map<ShowConditionDTO>(y),
                }));
                x.Conditions.RemoveAll(y => y.Duration <= 0);
            });
            return result;
        }

        private ShowBattleEventDTO HandleDefeat(Battle battle, GameMapAccessor mapAccessor)
        {
            var originalEnemy = battle.Participants.First(x => x.Enemy?.Tile is not null);
            originalEnemy.Enemy!.Health = originalEnemy.Enemy.MaxHealth;

            battle.Participants
                .Where(x => x.Enemy is not null && originalEnemy != x)
                .ForEach(x => _context.Remove(x.Enemy!));

            battle.Participants
                 .Where(x => x.Minion is not null)
                 .ForEach(x => _context.Remove(x.Minion!));

            var starterPlayer = battle.Participants.First(x => x.Character is not null && x.Character.Row == originalEnemy.Enemy.Tile.RowIdx && x.Character.Col == originalEnemy.Enemy.Tile.ColIdx);

            var targetTiles = mapAccessor.HexTiles.SelectMany(x => x).Where(x => x.TileContent == TileContent.City);
            var targetTile = targetTiles.MinBy(x => Math.Pow(x.ColIdx - starterPlayer.Character.Col.Value, 2) + Math.Pow(x.RowIdx - starterPlayer.Character.Row.Value, 2));

            battle.Participants.Where(x => x.Character is not null).ForEach(x =>
            {
                x.Character.IsInCombat = false;
                x.Character.CurrentHealth = x.Character.Health;
                x.Character.CurrentAbilityResource = x.Character.AbilityResource;
                x.Character.Row = targetTile.RowIdx;
                x.Character.Col = targetTile.ColIdx;
            });
            battle.NextInTurn = null;
            _context.SaveChanges();
            _context.Remove(battle);
            return new ShowBattleEventCombatEndDefeatDTO();
        }

        public async Task<ShowBattleStateDTO> GetCurrentBattleState()
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true, withBattle: true);
            if (gconn is null)
            {
                throw new UnauthorizedAccessException();
            }
            if (gconn.Battle is null)
            { 
                throw new InvalidOperationException();
            }

            var mappedBattle = _mapper.Map<ShowBattleStateDTO>(gconn.Battle);

            mappedBattle.TurnPredictions = _turnSimulator.Predict(gconn.Battle, 20);
            
            return mappedBattle;
        }

        public async  Task<ShowBattleStateDTO> MakePlayerTurn(CreatePlayerActionDTO playerAction)
        {
            var gconn = await GetGameConnectionDetailed(withGameCharacters: true, withCharacterDetail: true, withBattle: true, withMap:true);
            var battle = gconn.Battle;
            if (battle is null || !battle.HasStarted)
            {
                throw new InvalidOperationException();
            }

            if (battle.NextInTurn!.Character is null)
            {
                throw new InvalidOperationException();
            }
            var mapAccessor = new GameMapAccessor(gconn.Map);
            var participant = battle.NextInTurn!;
            var res = new List<ShowBattleEventDTO>();
            switch (playerAction)
            {
                case CreatePlayerActionSkipTurn:
                    res.AddRange(SkipPlayerTurn(battle, mapAccessor));
                    break;
                case CreatePlayerActionFlee:
                    res.AddRange(FleeBattle(battle, mapAccessor));
                    break;
                case CreatePlayerActionUseSkillOnEnemy skillAction:
                    if (skillAction.SkillSlotId is not null)
                    {
                        res.AddRange(PlayerUsesSkill(skillAction, battle, mapAccessor));
                    }
                    else {
                        res.AddRange(PlayerUsesAutoAttack(skillAction, battle, mapAccessor));
                    }
                    break;
                case CreatePlayerActionUseSkillOnAlly skillAction:
                    res.AddRange(PlayerUsesSkillOnAlly(skillAction, battle, mapAccessor));
                    break;
                case CreatePlayerActionUseSkillOnAny skillAction:
                    res.AddRange(PlayerUsesNonTargetedSkill(skillAction, battle, mapAccessor));
                    break;
                default:
                    throw new ArgumentException("Invalid player action.");
            }
            bool isVictory = res.Any(x => x is ShowBattleEventCombatEndVictoryDTO);
            bool isDefeated = res.Any(x => x is ShowBattleEventCombatEndDefeatDTO);
            if ( !isVictory && !isDefeated && battle.Participants.Where(x => (x.Enemy?.Health ?? 0) > 0).Count() == 0)
            {
                res.Add(HandleVictory(battle, mapAccessor));
            }

            await _context.SaveChangesAsync();
            var newBattleState = _mapper.Map<ShowBattleStateDTO>(battle);
            newBattleState.BattleEvents = res;
            newBattleState.TurnPredictions = isDefeated ? new () : _turnSimulator.Predict(battle, 10);
            return newBattleState;
        }

        private List<ShowBattleEventDTO> PlayerUsesNonTargetedSkill(CreatePlayerActionUseSkillOnAny skillAction, Battle battle, GameMapAccessor mapAccessor)
        {
            var skill = battle.NextInTurn.Character.SkillSlots.First(x => x.Id == skillAction.SkillSlotId);

            if (skill.CurrentCooldown > 0)
            {
                throw new InvalidOperationException("Skill on cooldown.");
            }

            if (battle.NextInTurn.Character.AbilityResource < skill.SkillHolder.Skill.ResourceCost)
            {
                throw new InvalidOperationException("Not enough resource.");
            }

            if (battle.NextInTurn.CurrentActionGauge < skill.SkillHolder.Skill.AbilityGaugeCost)
            {
                throw new InvalidOperationException("Not enough gauge.");
            }

            List<ShowBattleEventDTO> result = new();
            skill.CurrentCooldown = skill.SkillHolder.Skill.Cooldown;

            result.AddRange(SummonMinions(skill.SkillHolder.Skill.Summons, battle, battle.NextInTurn));

            if (battle.NextInTurn.CurrentActionGauge == 0)
            {
                result.AddRange(HandleNpcTurn(battle, mapAccessor));
            }

            return result;
        }

        private List<ShowBattleEventDTO> SummonMinions(ICollection<MinionBlueprint> summons, Battle battle, BattleParticipant caster)
        {
            var res = new List<ShowBattleEventDTO>();
            res.AddRange(summons.Select(x => Minion.Summon(x.Type, battle, caster, _mapper)));
            return res;
        }

        private ShowBattleEventDTO HandleVictory(Battle battle, GameMapAccessor mapAccessor)
        {
            var originalEnemy = battle.Participants.First(x => x.Enemy?.Tile is not null);
            var starterPlayer = battle.Participants.First(x => x.Character is not null && x.Character.Row == originalEnemy.Enemy.Tile.RowIdx && x.Character.Col == originalEnemy.Enemy.Tile.ColIdx);

            battle.Participants
                 .Where(x => x.Enemy is not null)
                 .ForEach(x => _context.Remove(x.Enemy!));

            battle.Participants
                 .Where(x => x.Minion is not null)
                 .ForEach(x => _context.Remove(x.Minion!));

            battle.Participants.Where(x => x.Character is not null).ForEach(x =>
            {
                x.Character.IsInCombat = false;
                x.Character.CurrentHealth = x.Character.Health;
                x.Character.CurrentAbilityResource = x.Character.AbilityResource;
            });
            battle.NextInTurn = null;
            _context.SaveChanges();
            _context.Remove(battle);
            return new ShowBattleEventCombatEndVictoryDTO();
        }

        private IEnumerable<ShowBattleEventDTO> PlayerUsesAutoAttack(CreatePlayerActionUseSkillOnEnemy skillAction, Battle battle, GameMapAccessor mapAccessor)
        {
            var enemy = battle.Participants.First(x => x.Id == skillAction.TargetId);

            List<ShowBattleEventDTO> result = new();

            if (!enemy.IsAlive)
            {
                throw new InvalidOperationException("Already dead.");
            }

            if (battle.NextInTurn.CurrentActionGauge < 1)
            {
                throw new InvalidOperationException("Not enough gauge.");
            }

            result.AddRange(battle.NextInTurn.Character.AutoAttack(enemy, battle.NextInTurn));

            if (battle.NextInTurn.CurrentActionGauge == 0)
            {
                result.AddRange(HandleNpcTurn(battle, mapAccessor));
            }


            return result;
        }

        private IEnumerable<ShowBattleEventDTO> SkipPlayerTurn(Battle battle, GameMapAccessor mapAccessor)
        {
            return HandleNpcTurn(battle, mapAccessor);
        }

        private IEnumerable<ShowBattleEventDTO> FleeBattle(Battle battle, GameMapAccessor mapAccessor)
        {
            var originalEnemy = battle.Participants.First(x => x.Enemy?.Tile is not null);
            originalEnemy.Enemy!.Health = originalEnemy.Enemy.MaxHealth;

            battle.Participants
                .Where(x => x.Enemy is not null && originalEnemy != x)
                .ForEach(x => _context.Remove(x.Enemy!));

            battle.Participants
                 .Where(x => x.Minion is not null)
                 .ForEach(x => _context.Remove(x.Minion!));

            var starterPlayer = battle.Participants.First(x => x.Character is not null && x.Character.Row == originalEnemy.Enemy.Tile.RowIdx && x.Character.Col == originalEnemy.Enemy.Tile.ColIdx);
            var tileWithNeighbours = mapAccessor.GetTileByIndex(starterPlayer.Character.Row.Value, starterPlayer.Character.Col.Value);
            var targetTiles = tileWithNeighbours.GetNeighbours().Where(x => x.IsWalkable() && x.Enemy is null).ToList();
            var targetTile = _rnd.Next(targetTiles);
            starterPlayer.Character.Row = targetTile.RowIdx;
            starterPlayer.Character.Col = targetTile.ColIdx;
            battle.Participants.Where(x => x.Character is not null).ForEach(x =>
            {
                x.Character.IsInCombat = false;
                x.Character.CurrentHealth = x.Character.Health;
                x.Character.CurrentAbilityResource = x.Character.AbilityResource;
            });
            battle.NextInTurn = null;
            _context.SaveChanges();
            _context.Remove(battle);
            return new List<ShowBattleEventDTO>() { new ShowBattleEventCombatEndFleeDTO() };
        }

        private IEnumerable<ShowBattleEventDTO> PlayerUsesSkillOnAlly(CreatePlayerActionUseSkillOnAlly skillAction, Battle battle, GameMapAccessor mapAccessor)
        {
            var skill = battle.NextInTurn.Character.SkillSlots.First(x => x.Id == skillAction.SkillSlotId);
            var target = battle.Participants.First(x => x.Id == skillAction.TargetId && x.Character is not null);

            if (skill.CurrentCooldown > 0)
            {
                throw new InvalidOperationException("Skill on cooldown.");
            }

            if (target.Character.CurrentHealth <= 0)
            {
                throw new InvalidOperationException("Already dead.");
            }

            if (battle.NextInTurn.Character.AbilityResource < skill.SkillHolder.Skill.ResourceCost)
            {
                throw new InvalidOperationException("Not enough resource.");
            }

            if (battle.NextInTurn.CurrentActionGauge < skill.SkillHolder.Skill.AbilityGaugeCost)
            {
                throw new InvalidOperationException("Not enough gauge.");
            }

            if (skill.SkillHolder.Skill.TargetType != TargetType.Ally)
            {
                throw new InvalidOperationException("Invalid target for skill.");
            }

            List<ShowBattleEventDTO> result = new();

            if (skill.SkillHolder.Skill.Damage > 0)
            {
                result.AddRange(skill.SkillHolder.Skill.ActivateHeal(target, battle.NextInTurn, _mapper));
            }
            else
            {
                result.AddRange(skill.SkillHolder.Skill.ActivateBuffOnly(target, battle.NextInTurn, _mapper));
            }

            var buffs = skill.SkillHolder.Skill.Buffs;
            result.AddRange(ApplyBuffs(buffs, target, battle.NextInTurn));
            skill.CurrentCooldown = skill.SkillHolder.Skill.Cooldown;

            if (battle.NextInTurn.CurrentActionGauge == 0)
            {
                result.AddRange(HandleNpcTurn(battle, mapAccessor));
            }

            return result;
        }

        private IEnumerable<ShowBattleEventDTO> ApplyConditions(ICollection<ConditionBlueprint> conditionBps, BattleParticipant target, BattleParticipant source)
        {
            var conditions = conditionBps.SelectMany<ConditionBlueprint, Condition>(x =>
            {
                var res = new List<Condition>();
                for (int i = 0; i < x.Stacks; i++)
                {
                    switch (x.ConditionType)
                    {
                        case ConditionType.Bleed:
                            res.Add( new Bleed()
                            {
                                ID = Guid.NewGuid(),
                                Duration = x.Duration,
                                BattleParticipant = target,
                                ConditionDamageMultiplier = source.Character.AbilityDamage * 0.5,
                                Caster = source
                            });
                            break;
                        case ConditionType.Weakness:
                            res.Add( new Weakness()
                            {
                                ID = Guid.NewGuid(),
                                Duration = x.Duration,
                                BattleParticipant = target,
                                Caster = source
                            });
                            break;
                        case ConditionType.Taunt:
                            res.Add(new Taunt()
                            {
                                ID = Guid.NewGuid(),
                                Duration = x.Duration,
                                BattleParticipant = target,
                                Caster = source
                            });
                            break;
                        default: throw new InvalidOperationException();
                    }
                }
                return res;
            }).ToList();

            conditions.ForEach(x =>
            {
                if (x.StacksDuration)
                {
                    var oldCondition = target.Conditions.FirstOrDefault(y => y.ConditionType == x.ConditionType);
                    if (oldCondition is not null)
                    {
                        oldCondition.Duration += x.Duration;
                        x.Duration = oldCondition.Duration;
                        x.ID = oldCondition.ID;
                        x.ConditionDamageMultiplier = oldCondition.ConditionDamageMultiplier;
                        oldCondition.Caster = source;
                    }
                    else
                    {
                        target.Conditions.Add(x);
                    }
                }
                else
                {
                    target.Conditions.Add(x);
                }
            });
            var res = new List<ShowBattleEventApplyConditionDTO>();
            res.Add(new ShowBattleEventApplyConditionDTO
            {
                Conditions = _mapper.Map<List<ShowConditionDTO>>(conditions),
                SourceParticipantId = source.Id,
                TargetParticipantId = target.Id
            });
            return res;
        }

        private IEnumerable<ShowBattleEventDTO> ApplyBuffs(ICollection<BuffBlueprint> buffBps, BattleParticipant target, BattleParticipant source)
        {
            var buffs = buffBps.SelectMany<BuffBlueprint, Buff>(x =>
            {
                var res = new List<Buff>();
                for (int i = 0; i < x.Stacks; i++)
                {
                    switch (x.BuffType)
                    {
                        case BuffType.Rejuvenation:
                            res.Add(new Rejuvenation()
                            {
                                ID = Guid.NewGuid(),
                                Duration = x.Duration,
                                BattleParticipant = target, 
                                Caster = source
                            });
                            break;
                        case BuffType.Might:
                            res.Add( new Might()
                            {
                                ID = Guid.NewGuid(),
                                Duration = x.Duration,
                                BattleParticipant = target,
                                Caster = source
                            });
                            break;
                        case BuffType.DefenseUp:
                            res.Add(new DefenseUp()
                            {
                                ID = Guid.NewGuid(),
                                Duration = x.Duration,
                                BattleParticipant = target,
                                Caster = source
                            });
                            break;
                        case BuffType.Regeneration:
                            res.Add(new Regeneration()
                            {
                                ID = Guid.NewGuid(),
                                Duration = x.Duration,
                                BattleParticipant = target,
                                Caster = source
                            });
                            break;
                        default: throw new InvalidOperationException();
                    }
                }
                return res;
            }).ToList();

            buffs.ForEach(x =>
            {
                if (x.StacksDuration)
                {
                    var oldBuff = target.Buffs.FirstOrDefault(y => y.BuffType == x.BuffType);
                    if (oldBuff is not null)
                    {
                        oldBuff.Duration += x.Duration;
                        x.Duration = oldBuff.Duration;
                        x.ID = oldBuff.ID;
                        oldBuff.Caster = source;
                    }
                    else
                    {
                        target.Buffs.Add(x);
                    }
                }
                else
                {
                    target.Buffs.Add(x);
                }
            });

            var res = new List<ShowBattleEventApplyBuffDTO>();
            res.Add(new ShowBattleEventApplyBuffDTO
            {
                Buffs = _mapper.Map<List<ShowBuffDTO>>(buffs),
                SourceParticipantId = source.Id,
                TargetParticipantId = target.Id
            });
            return res;
        }

        private IEnumerable<ShowBattleEventDTO> PlayerUsesSkill(CreatePlayerActionUseSkillOnEnemy skillAction, Battle battle, GameMapAccessor mapAccessor)
        {
            var skill = battle.NextInTurn.Character.SkillSlots.First(x => x.Id == skillAction.SkillSlotId);
            var enemy = battle.Participants.First(x => x.Id == skillAction.TargetId);

            if (skill.CurrentCooldown > 0)
            {
                throw new InvalidOperationException("Skill on cooldown.");
            }

            if (!enemy.IsAlive)
            {
                throw new InvalidOperationException("Already dead.");
            }

            if (battle.NextInTurn.Character.AbilityResource < skill.SkillHolder.Skill.ResourceCost)
            {
                throw new InvalidOperationException("Not enough resource.");
            }

            if (battle.NextInTurn.CurrentActionGauge < skill.SkillHolder.Skill.AbilityGaugeCost)
            {
                throw new InvalidOperationException("Not enough gauge.");
            }

            List<ShowBattleEventDTO> result = new();
            result.AddRange(skill.SkillHolder.Skill.Activate(enemy, battle.NextInTurn, _mapper));
            skill.CurrentCooldown = skill.SkillHolder.Skill.Cooldown;

            result.AddRange(ApplyConditions(skill.SkillHolder.Skill.Conditions, enemy ,battle.NextInTurn));

            if (battle.NextInTurn.CurrentActionGauge == 0)
            {
                result.AddRange(HandleNpcTurn(battle, mapAccessor));
            }

            return result;
        }
    }
}
