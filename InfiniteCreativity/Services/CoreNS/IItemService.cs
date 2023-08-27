using InfiniteCreativity.DTO;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface IItemService
    {
        public Task DeleteItems(DeleteItemsDTO items);
        public Task<IEnumerable<ShowItemDTO>> GetAllItems();
    }
}
