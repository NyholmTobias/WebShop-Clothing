using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<ActionResult<ItemResponse>> CreateItem(ItemRequest itemRequest)
        {
            var response =  await _itemService.CreateItem(itemRequest);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemResponse>>> GetAllItems()
        {
            var response = await _itemService.GetAllItems();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<Guid>> DeleteItem([FromRoute] Guid itemId)
        {
            var response = await _itemService.DeleteItem(itemId);
            return response == Guid.Empty ? BadRequest(response) : Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ItemResponse>> UpdateItem(ItemRequest itemRequest)
        {
            var response = await _itemService.UpdateItem(itemRequest);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemResponse>> GetItemById(Guid itemId)
        {
            var response = await _itemService.GetItemById(itemId);
            return response != null ? Ok(response) : BadRequest(response);
        }
    }
}
