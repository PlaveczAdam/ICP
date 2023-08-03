using InfiniteCreativity.Models.DTO;

namespace InfiniteCreativity.Services
{
    public interface IListingService
    {
        public Task<ShowListingDTO> CreateListing(CreateListingDTO newListing);
        public Task<IEnumerable<ShowListingDTO>> GetListings(ListingFilterDTO listingFilter);
        public Task PurchaseListing(int id);
        public Task CancelListing(int id);
    }
}
