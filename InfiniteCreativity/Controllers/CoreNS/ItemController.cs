using InfiniteCreativity.Data;
using InfiniteCreativity.DTO;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Weapons;
using InfiniteCreativity.Services.CoreNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers.CoreNS
{
    [Authorize]
    [ApiController, Route("/api/item")]
    public class ItemController : Controller
    {
        private IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShowItemDTO>> GetAllItems()
        {
            var newItem = await _itemService.GetAllItems();
            return newItem;
        }

        [HttpDelete]
        public async Task DeleteItems([FromBody] DeleteItemsDTO items)
        {
            await _itemService.DeleteItems(items);
        }
    }
}
