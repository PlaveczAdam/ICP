using InfiniteCreativity.Data;
using InfiniteCreativity.Models;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Repositorys
{
    public class QuestRepository : IQuestRepository
    {
        private InfiniteCreativityContext _context;

        public QuestRepository(InfiniteCreativityContext context)
        {
            _context = context;
        }

        public async Task<Quest> CreateQuest(Quest quest)
        {
            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();
            return quest;
        }

        public async Task<Quest> UpdateQuest(Quest quest)
        {
            await _context.SaveChangesAsync();
            return quest;
        }

        public Task<Quest> GetQuestById(int questId)
        {
            return _context.Quest
                .Include(x => x.Rewards)
                .FirstOrDefaultAsync(x => x.Id == questId)!;
        }
    }
}
