using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Services
{
    public interface IItemService
    {
        public Task DeleteItems(DeleteItemsDTO items);
        public Task<IEnumerable<ShowItemDTO>> GetAllItems();
    }
}
