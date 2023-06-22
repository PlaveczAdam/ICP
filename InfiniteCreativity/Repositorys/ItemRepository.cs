using InfiniteCreativity.Data;
using InfiniteCreativity.Models;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Repositorys
{
    public class ItemRepository : IItemRepository
    {
        private InfiniteCreativityContext _context;

        public ItemRepository(InfiniteCreativityContext context)
        {
            _context = context;
        }

        public async Task<Item> CreateItem(Item item)
        {
            _context.Item.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public Task<Item> DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(int itemId, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> UpdateItems(IEnumerable<Item> items)
        {
            await _context.SaveChangesAsync();
            return items;
        }
    }
}
