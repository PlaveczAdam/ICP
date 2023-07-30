using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers
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
        public async Task<ShowListingDTO> CreateListing([FromBody]CreateListingDTO newListing)
        {
            return await _listingService.CreateListing(newListing);
        }

        [HttpGet]
        public async Task<IEnumerable<ShowListingDTO>> GetListings()
        {
            return await _listingService.GetListings();
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
