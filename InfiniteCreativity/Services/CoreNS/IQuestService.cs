using InfiniteCreativity.DTO;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface IQuestService
    {
        public Task<IEnumerable<ShowQuestDTO>> GetQuestByCharacterId(Guid characterId);
        public Task<ShowQuestDTO> CreateQuest(Guid characterId);
        public Task<ShowQuestDTO> MakeQuestProgress(Guid questId, int amount);
        public Task TickQuests();
    }
}
