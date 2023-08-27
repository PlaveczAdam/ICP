using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Models.CoreNS;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace InfiniteCreativity.Services.CoreNS
{
    public class ListingService : IListingService
    {
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;
        private IPlayerService _playerService;
        private IMapper _mapper;
        private INotificationService _notificationService;

        public ListingService(InfiniteCreativityContext context, IMapper mapper, ICharacterService characterService, IPlayerService playerService, INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _characterService = characterService;
            _playerService = playerService;
            _notificationService = notificationService;
        }

        public async Task<ShowListingDTO> CreateListing(CreateListingDTO newListing)
        {
            var player = await _playerService.GetCurrentPlayer(true);
            var newL = new Listing();
            newL.Price = newListing.Price;
            newL.Seller = player;
            var item = player.Inventory!.Single(x => x.Id == newListing.ItemId);
            item.Player = null;
            newL.Item = item;
            newL.ListingDate = DateTime.UtcNow;
            await _characterService.UnequipItemFromAllCharacter(newListing.ItemId);
            _context.Listing.Add(newL);
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(player.Id);
            return _mapper.Map<ShowListingDTO>(newL);
        }

        public async Task<IEnumerable<ShowListingDTO>> GetListings(ListingFilterDTO listingFilter)
        {
            IQueryable<Listing> ltings = _context.Listing.Include((x) => x.Seller).Include((x) => x.Item);
            if (listingFilter is not null)
            {
                if (listingFilter.SellerId is not null)
                {
                    ltings = ltings.Where((x) => x.Seller.Id == listingFilter.SellerId);
                }
                if (listingFilter.NotSellerId is not null)
                {
                    ltings = ltings.Where((x) => x.Seller.Id != listingFilter.NotSellerId);
                }
            }
            var listingResult = await ltings.ToListAsync();
            return _mapper.Map<List<ShowListingDTO>>(listingResult);
        }

        public async Task PurchaseListing(int id)
        {
            var lting = await _context.Listing
                    .Include(x => x.Seller)
                    .Include(x => x.Item)
                    .SingleAsync(x => x.Id == id);
            var player = await _playerService.GetCurrentPlayer();
            if (player.Money < lting.Price)
            {
                throw new InvalidOperationException();
            }
            player.Money -= lting.Price;
            lting.Seller.Money += lting.Price;
            lting.Item.Player = player;
            _context.Remove(lting);
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(player.Id);
            await _notificationService.SendGNotification(lting.Seller.Id);
        }
        public async Task CancelListing(int id)
        {
            var lting = await _context.Listing
                    .Include(x => x.Seller)
                    .Include(x => x.Item)
                    .SingleAsync(x => x.Id == id);
            var player = await _playerService.GetCurrentPlayer();

            if (player.Id != lting.Seller.Id)
            {
                throw new InvalidOperationException();
            }

            lting.Item.Player = player;
            _context.Remove(lting);
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(player.Id);

        }
    }
}
