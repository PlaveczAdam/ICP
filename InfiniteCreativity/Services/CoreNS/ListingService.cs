using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Materials;
using InfiniteCreativity.Models.CoreNS.Weapons;
using InfiniteCreativity.Models.Enums.CoreNS;
using Microsoft.EntityFrameworkCore;

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
            Item listedItem = item;
            
            if(item is Stackable stackable)
            {
                if (stackable.Amount < newListing.Amount)
                {
                    throw new InvalidOperationException();
                }

                if (stackable.Amount > newListing.Amount)
                {
                    listedItem = stackable.Split(newListing.Amount);
                }
            }

            listedItem.Player = null;
            newL.Item = listedItem;
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

        public async Task PurchaseListing(int id, ulong amount)
        {
            var lting = await _context.Listing
                    .Include(x => x.Seller)
                    .Include(x => x.Item)
                    .SingleAsync(x => x.Id == id);
            var player = await _playerService.GetCurrentPlayer(withInventory:true);
            if (player.Money < lting.Price*amount)
            {
                throw new InvalidOperationException();
            }
            player.Money -= lting.Price * amount;
            lting.Seller.Money += lting.Price * amount;

            var itemInPlayerInventory = player.Inventory.FirstOrDefault(x => {
                if (x is Stackable stackable)
                {
                    if (lting.Item is Stackable stkble)
                    { 
                        return stackable.StackableType == stkble.StackableType;
                    }
                }
                return false;
            });
            Item listedItem = lting.Item;

            if (listedItem is Stackable stackable)
            {
                if (stackable.Amount < amount)
                {
                    throw new InvalidOperationException();
                }
                else
                if (stackable.Amount > amount)
                {
                    stackable.Amount -= amount;
                    if (itemInPlayerInventory is Stackable stackableAtPlayer)
                    {
                        stackableAtPlayer.Amount += amount;
                        listedItem = stackableAtPlayer;
                    }
                    else
                    {
                        listedItem = stackable.Split(amount);
                    }
                }
                else
                {
                    if (itemInPlayerInventory is Stackable stackableAtPlayer)
                    {
                        stackableAtPlayer.Amount += amount;
                        listedItem = stackableAtPlayer;
                        _context.Remove(lting.Item);
                    }
                    else
                    {
                        listedItem = lting.Item;
                    }
                    _context.Remove(lting);
                }

            }
            listedItem.Player = player;

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
            var player = await _playerService.GetCurrentPlayer(withInventory:true);

            if (player.Id != lting.Seller.Id)
            {
                throw new InvalidOperationException();
            }

            var itemInPlayerInventory = player.Inventory.FirstOrDefault(x => {
                if (x is Stackable stackable)
                {
                    if (lting.Item is Stackable stkble)
                    {
                        return stackable.StackableType == stkble.StackableType;
                    }
                }
                return false;
            });
            Item listedItem = lting.Item;

            if (listedItem is Stackable stackable)
            {
                if (itemInPlayerInventory is Stackable stackableAtPlayer)
                {
                    listedItem = stackableAtPlayer;
                    stackableAtPlayer.Amount += stackable.Amount;
                    _context.Remove(stackable);
                }
            }
            listedItem.Player = player;
            _context.Remove(lting);
            await _context.SaveChangesAsync();
            await _notificationService.SendGNotification(player.Id);

        }
    }
}
