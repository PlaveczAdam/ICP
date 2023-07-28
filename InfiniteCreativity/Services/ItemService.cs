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
        private IPlayerService _playerService;

        public ItemService(
            InfiniteCreativityContext context,
            IMapper mapper,
            IPlayerService playerService)
        {
            _context = context;
            _mapper = mapper;
            _playerService = playerService;
        }

        public async Task<IEnumerable<ShowItemDTO>> GetAllItems()
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var contItem = _context.Item.Where((x)=>x.Player!=null && x.Player.Id==currentPlayer.Id).ToList();
            var mappedItem = _mapper.Map<List<ShowItemDTO>>(contItem);
            return mappedItem;
        }

        [Obsolete]
        public async Task<IEnumerable<ShowItemDTO>> GetAllItemsByType(ItemType itemType)
        {
            return await _context.Item
                .Where(x => x.ItemType == itemType)
                .Select(x => _mapper.Map<ShowItemDTO>(x))
                .ToListAsync();
        }
    }
}
