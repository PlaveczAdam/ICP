using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace InfiniteCreativity.Services
{
    public class ListingService : IListingService
    {
        private InfiniteCreativityContext _context;
        private ICharacterService _characterService;
        private IPlayerService _playerService;
        private IMapper _mapper;

        public ListingService(InfiniteCreativityContext context, IMapper mapper, ICharacterService characterService, IPlayerService playerService)
        {
            _context = context;
            _mapper = mapper;
            _characterService = characterService;
            _playerService = playerService;
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
            return _mapper.Map<ShowListingDTO>(newL);
        }

        public async Task<IEnumerable<ShowListingDTO>> GetListings()
        {
            var ltings = await _context.Listing.Include((x) => x.Seller).Include((x) => x.Item).ToListAsync();
            return _mapper.Map<List<ShowListingDTO>>(ltings);
        }

        public async Task PurchaseListing(int id)
        {
            var lting = await _context.Listing
                    .Include(x=>x.Seller)
                    .Include(x => x.Item)
                    .SingleAsync(x=>x.Id==id);
            var player = await _playerService.GetCurrentPlayer();
            if (player.Purse < lting.Price)
            {
                throw new InvalidOperationException();
            }
            player.Purse -= lting.Price;
            lting.Seller.Purse += lting.Price;
            lting.Item.Player = player;
            _context.Remove(lting);
            await _context.SaveChangesAsync();
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

        }
    }
}
