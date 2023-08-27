using InfiniteCreativity.DTO;
using InfiniteCreativity.Services.CoreNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers.CoreNS
{
    [Authorize]
    [ApiController, Route("api/listing")]
    public class ListingController : ControllerBase
    {
        private IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpPost]
        public async Task<ShowListingDTO> CreateListing([FromBody] CreateListingDTO newListing)
        {
            return await _listingService.CreateListing(newListing);
        }

        [HttpGet]
        public async Task<IEnumerable<ShowListingDTO>> GetListings([FromQuery] ListingFilterDTO listingFilter)
        {
            return await _listingService.GetListings(listingFilter);
        }

        [HttpPut, Route("sold/{id}")]
        public async Task PurchaseListing(int id)
        {
            await _listingService.PurchaseListing(id);
        }

        [HttpPut, Route("cancelled/{id}")]
        public async Task CancelListing(int id)
        {
            await _listingService.CancelListing(id);
        }
    }
}
