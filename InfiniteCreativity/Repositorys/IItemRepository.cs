using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Repositorys
{
    public interface IItemRepository
    {
        public Task<Item> CreateItem(Item item);
        public Task<Item> UpdateItem(int itemId, Item item);
        public Task<Item> DeleteItem(int itemId);
        public Task<IEnumerable<Item>> UpdateItems(IEnumerable<Item> items);
    }
}
