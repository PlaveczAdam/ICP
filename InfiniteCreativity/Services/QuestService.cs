using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Services.QuestGeneratorNS;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services
{
    public class QuestService : IQuestService
    {
        private IPlayerService _playerService;
        private IMapper _mapper;
        private QuestGenerator _questGenerator;
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;

        public QuestService(
            IMapper mapper,
            IPlayerService playerService,
            QuestGenerator questGenerator
,
            InfiniteCreativityContext context,
            ICharacterService characterService)
        {
            _mapper = mapper;
            _playerService = playerService;
            _questGenerator = questGenerator;
            _context = context;
            _characterService = characterService;
        }

        public async Task<IEnumerable<ShowQuestDTO>> GetQuestByCharacterId(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = await _characterService.GetCharacterById(characterId, currentPlayer);

            var quests = _context.Quest.Include((x)=>x.Rewards).Where(x => x.Character.Id == character.Id);

            return _mapper.Map<IEnumerable<ShowQuestDTO>>(quests);
        }

        public async Task<ShowQuestDTO> MakeQuestProgress(int questId, int amount)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var q =
                await _context.Quest
                .Include(x => x.Rewards)
                .FirstOrDefaultAsync(x => x.Id == questId)!
                ?? throw new UnauthorizedOperationException();
            var character = await _characterService.GetCharacterById(q.Character.Id, currentPlayer);
            if (q.IsDone)
            {
                throw new UnauthorizedOperationException();
            }

            q.Progression = Math.Clamp(q.Progression + amount, 0, 100);

            HandleQuestCompletion(q, currentPlayer, character);
            await _context.SaveChangesAsync();
            return _mapper.Map<ShowQuestDTO>(q);
        }

        public async Task<ShowQuestDTO> CreateQuest(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = await  _characterService.GetCharacterById(characterId, currentPlayer, withQuest:true);
            var numberOfQuestTaken = character!.Quests!.Where((x)=>!x.IsDone).Count();
            if (numberOfQuestTaken >= currentPlayer.QuestSlot)
            { 
                throw new LimitReachedException();
            }

            var quest = _questGenerator.Generate();
            quest.Character = character;

            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();

            return _mapper.Map<ShowQuestDTO>(quest);
        }

        public async Task TickQuests()
        {
            await _context.Quest
                .Include((x)=>x.Character)
                .ThenInclude((x)=>x.Player)
                .Include((x)=> x.Rewards)
                .Where((x)=>!x.IsDone)
                .ForEachAsync((x) => {
                    var newProgress = (TimeSpan.FromMinutes(1) / x.Duration)*100;
                    x.Progression = Math.Clamp(x.Progression + newProgress, 0, 100);
                    HandleQuestCompletion(x, x.Character.Player, x.Character);
            });
            await _context.SaveChangesAsync();
        }
        private void HandleQuestCompletion(Quest quest, Player currentPlayer, Character character)
        {
            if (quest.Progression == 100)
            {
                quest.Rewards.ToList().ForEach(x => x.Player = currentPlayer);
                currentPlayer.Money += quest.CashReward;
                character.Level += quest.LevelReward;
                quest.IsDone = true;
            }
        }
    }
}
