using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopAPI.Controllers.V1
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<OrderResponse>> CreateOrder(OrderRequest orderRequest)
        {
            var response = await _orderService.CreateOrder(orderRequest);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
        {
            var response = await _orderService.GetAllOrders();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById(Guid orderId)
        {
            var response = await _orderService.GetOrderById(orderId);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<OrderResponse>> UpdateOrder ( OrderRequest orderRequest)
        {
            var response = await _orderService.UpdateOrder(orderRequest);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult<OrderResponse>> UpdateOrder(Guid orderId)
        {
            var response = await _orderService.DeleteOrder(orderId);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderResponse>>> GetOrdersByUserId(Guid userId)
        {
            var response = await _orderService.GetOrdersByUser(userId);
            return response != null ? Ok(response) : BadRequest(response);
        }
    }
}
