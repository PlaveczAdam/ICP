using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Services.CoreNS
{
    public class ItemService : IItemService
    {
        private InfiniteCreativityContext _context;
        private IMapper _mapper;
        private IPlayerService _playerService;
        private INotificationService _notificationService;

        public ItemService(
            InfiniteCreativityContext context,
            IMapper mapper,
            IPlayerService playerService,
            INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _playerService = playerService;
            _notificationService = notificationService;
        }

        public async Task DeleteItems(DeleteItemsDTO items)
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(true);
            var itemsToDelete = currentPlayer!.Inventory!.Where((x) => items.Items.Contains(x.Id));
            currentPlayer.Money += itemsToDelete.Sum(x => x.Value) ?? 0;
            _context.RemoveRange(itemsToDelete);
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(currentPlayer.Id);
        }

        public async Task<IEnumerable<ShowItemDTO>> GetAllItems()
        {
            var currentPlayer = await _playerService.GetCurrentPlayer();
            var contItem = await _context.Item.Where((x) => x.Player != null && x.Player.Id == currentPlayer.Id).OrderBy(x => x.Id).ToListAsync();
            var mappedItem = _mapper.Map<List<ShowItemDTO>>(contItem);
            return mappedItem;
        }
    }
}
