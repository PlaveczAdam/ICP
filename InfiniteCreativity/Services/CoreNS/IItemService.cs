using InfiniteCreativity.DTO;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface IItemService
    {
        public Task DeleteItems(DeleteItemsDTO items);
        public Task<IEnumerable<ShowItemDTO>> GetAllItems();
    }
}
