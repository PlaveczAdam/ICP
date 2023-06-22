using AutoMapper;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Repositorys;
using InfiniteCreativity.Services.QuestGeneratorNS;

namespace InfiniteCreativity.Services
{
    public class QuestService : IQuestService
    {
        private IQuestRepository _questRepository;
        private IPlayerService _playerService;
        private IMapper _mapper;
        private QuestGenerator _questGenerator;
        private IItemRepository _itemRepository;

        public QuestService(
            IQuestRepository questRepository,
            IMapper mapper,
            IPlayerService playerService,
            QuestGenerator questGenerator,
            IItemRepository itemRepository
        )
        {
            _questRepository = questRepository;
            _mapper = mapper;
            _playerService = playerService;
            _questGenerator = questGenerator;
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<ShowQuestDTO>> GetQuestByCharacterId(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = GetCharacterById(characterId, currentPlayer);

            return _mapper.Map<IEnumerable<ShowQuestDTO>>(character.Quests);
        }

        public async Task<ShowQuestDTO> MakeQuestProgress(int questId, int amount)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var q =
                await _questRepository.GetQuestById(questId)
                ?? throw new UnauthorizedOperationException();
            var character = GetCharacterById(q.Character.Id, currentPlayer);
            if (q.IsDone)
            {
                throw new UnauthorizedOperationException();
            }

            q.Progression = Math.Clamp(q.Progression + amount, 0, 100);

            if (q.Progression == 100)
            {
                q.Rewards.ToList().ForEach(x => x.Character = character);
                q.IsDone = true;
                await _itemRepository.UpdateItems(q.Rewards);
            }
            return _mapper.Map<ShowQuestDTO>(await _questRepository.UpdateQuest(q));
        }

        public async Task<ShowQuestDTO> CreateQuest(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = GetCharacterById(characterId, currentPlayer);
            var quest = _questGenerator.Generate();
            quest.Character = character;
            quest = await _questRepository.CreateQuest(quest);
            return _mapper.Map<ShowQuestDTO>(quest);
        }

        private Character GetCharacterById(int characterId, Player currentPlayer)
        {
            var character =
                currentPlayer.Characters.FirstOrDefault(x => x.Id == characterId)
                ?? throw new UnauthorizedOperationException();
            return character;
        }
    }
}
