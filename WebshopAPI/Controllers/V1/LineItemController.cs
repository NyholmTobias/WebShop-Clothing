using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LineItemController : Controller
    {
        private readonly ILineItemService _lineItemService;
        public LineItemController(ILineItemService lineItemService)
        {
            _lineItemService = lineItemService;
        }

        //Use CreateOrder instead.
        //[HttpPost]
        //public async Task<ActionResult<LineItemResponse>> CreateLineItem(LineItemRequest lineItemRequest)
        //{
        //    var response = await _lineItemService.CreateLineItem(lineItemRequest);
        //    return response != null ? Ok(response) : BadRequest(response);
        //}
    }
}
