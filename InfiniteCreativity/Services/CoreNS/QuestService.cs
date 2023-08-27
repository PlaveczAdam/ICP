using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Services.CoreNS.QuestGeneratorNS;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services.CoreNS
{
    public class QuestService : IQuestService
    {
        private IPlayerService _playerService;
        private IMapper _mapper;
        private QuestGenerator _questGenerator;
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;
        private INotificationService _notificationService;

        public QuestService(
            IMapper mapper,
            IPlayerService playerService,
            QuestGenerator questGenerator
,
            InfiniteCreativityContext context,
            ICharacterService characterService,
            INotificationService notificationService)
        {
            _mapper = mapper;
            _playerService = playerService;
            _questGenerator = questGenerator;
            _context = context;
            _characterService = characterService;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<ShowQuestDTO>> GetQuestByCharacterId(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = await _characterService.GetCharacterById(characterId, currentPlayer);

            var quests = _context.Quest.Include((x) => x.Rewards).Where(x => x.Character.Id == character.Id);

            return _mapper.Map<IEnumerable<ShowQuestDTO>>(quests);
        }

        public async Task<ShowQuestDTO> MakeQuestProgress(int questId, int amount)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(withInventory: true);
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
            await _notificationService.SendGNotification(currentPlayer.Id);

            return _mapper.Map<ShowQuestDTO>(q);
        }

        public async Task<ShowQuestDTO> CreateQuest(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = await _characterService.GetCharacterById(characterId, currentPlayer, withQuest: true);
            var numberOfQuestTaken = character!.Quests!.Where((x) => !x.IsDone).Count();
            if (numberOfQuestTaken >= currentPlayer.QuestSlot)
            {
                throw new LimitReachedException();
            }

            var quest = _questGenerator.Generate();
            quest.Character = character;

            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(currentPlayer.Id);

            return _mapper.Map<ShowQuestDTO>(quest);
        }

        public async Task TickQuests()
        {
            await _context.Quest
                .Include((x) => x.Character)
                .ThenInclude((x) => x.Player)
                .ThenInclude((x) => x.Inventory)
                .Include((x) => x.Rewards)
                .Where((x) => !x.IsDone)
                .ForEachAsync((x) =>
                {
                    var newProgress = TimeSpan.FromMinutes(1) / x.Duration * 100;
                    x.Progression = Math.Clamp(x.Progression + newProgress, 0, 100);
                    HandleQuestCompletion(x, x.Character.Player, x.Character);
                });
            await _context.SaveChangesAsync();
        }
        private void HandleQuestCompletion(Quest quest, Player currentPlayer, Character character)
        {
            if (quest.Progression >= 99.9)
            {
                quest.Progression = 100;
                quest.Rewards.ToList().ForEach(x =>
                {
                    if (x is Stackable stackableReward)
                    {
                        Stackable? playerStack = (Stackable?)currentPlayer!.Inventory!.SingleOrDefault((y) =>
                        {
                            if (y is Stackable stackable)
                            {
                                return stackable.StackableType == stackableReward.StackableType;
                            }
                            return false;
                        });
                        if (playerStack is null)
                        {
                            x.Player = currentPlayer;
                        }
                        else
                        {
                            playerStack.Amount++;
                            _context.Remove(x);
                        }
                    }
                    else
                    {
                        x.Player = currentPlayer;
                    }
                });
                currentPlayer.Money += quest.CashReward;
                character.Level += quest.LevelReward;
                quest.IsDone = true;
            }
        }
    }
}
