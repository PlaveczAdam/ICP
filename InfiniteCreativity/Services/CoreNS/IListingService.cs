using InfiniteCreativity.DTO;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface IListingService
    {
        public Task<ShowListingDTO> CreateListing(CreateListingDTO newListing);
        public Task<IEnumerable<ShowListingDTO>> GetListings(ListingFilterDTO listingFilter);
        public Task PurchaseListing(Guid id, ulong amount);
        public Task CancelListing(Guid id);
    }
}
