using InfiniteCreativity.Models;

namespace InfiniteCreativity.Repositorys
{
    public interface IQuestRepository
    {
        public Task<Quest> CreateQuest(Quest quest);
        public Task<Quest> UpdateQuest(Quest quest);
        public Task<Quest> GetQuestById(int questId);
    }
}
