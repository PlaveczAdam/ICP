using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services
{
    public class ItemService : IItemService
    {
        private InfiniteCreativityContext _context;
        private IMapper _mapper;

        public ItemService(
            InfiniteCreativityContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowItemDTO>> GetAllItems()
        {
            var contItem = _context.Item.ToList();
            var mapped = _mapper.Map<List<ShowItemDTO>>(contItem);
            return mapped;
        }

        public async Task<IEnumerable<ShowItemDTO>> GetAllItemsByType(ItemType itemType)
        {
            return await _context.Item
                .Where(x => x.ItemType == itemType)
                .Select(x => _mapper.Map<ShowItemDTO>(x))
                .ToListAsync();
        }
    }
}
