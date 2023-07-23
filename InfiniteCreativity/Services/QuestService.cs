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

        public QuestService(
            IMapper mapper,
            IPlayerService playerService,
            QuestGenerator questGenerator
,
            InfiniteCreativityContext context)
        {
            _mapper = mapper;
            _playerService = playerService;
            _questGenerator = questGenerator;
            _context = context;
        }

        public async Task<IEnumerable<ShowQuestDTO>> GetQuestByCharacterId(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = GetCharacterById(characterId, currentPlayer);

            var quests = _context.Quest.Where(x => x.Character.Id == character.Id);

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
            var character = GetCharacterById(q.Character.Id, currentPlayer);
            if (q.IsDone)
            {
                throw new UnauthorizedOperationException();
            }

            q.Progression = Math.Clamp(q.Progression + amount, 0, 100);

            if (q.Progression == 100)
            {
                q.Rewards.ToList().ForEach(x => x.Player = currentPlayer);
                q.IsDone = true;

                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ShowQuestDTO>(q);
        }

        public async Task<ShowQuestDTO> CreateQuest(int characterId)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var character = GetCharacterById(characterId, currentPlayer);
            var quest = _questGenerator.Generate();
            quest.Character = character;

            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();

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
