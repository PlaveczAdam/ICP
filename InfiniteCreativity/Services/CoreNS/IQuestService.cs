using InfiniteCreativity.DTO;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface IQuestService
    {
        public Task<IEnumerable<ShowQuestDTO>> GetQuestByCharacterId(int characterId);
        public Task<ShowQuestDTO> CreateQuest(int characterId);
        public Task<ShowQuestDTO> MakeQuestProgress(int questId, int amount);
        public Task TickQuests();
    }
}
