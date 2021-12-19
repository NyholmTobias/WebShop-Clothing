using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopServices.Validation.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IValidatorFactory _validatorFactory;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IItemRepository itemRepository, IValidatorFactory validatorFactory)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _validatorFactory = validatorFactory;
        }
        public async Task<OrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            var orderResponse = new OrderResponse();

            orderResponse.AddValidationResult(await _validatorFactory.Get("OrderRequestValidator").Validate(orderRequest));
            if (orderResponse.Success)
            {
                Order order = new()
                {
                    LineItems = _mapper.Map<List<LineItem>>(orderRequest.LineItems),
                    Status = Statuses.Accepted,
                    UserId = orderRequest.UserId,
                    Username = orderRequest.Username,
                    
                };
                order.TotalPrice = CalculateTotalOrderPrice(order.LineItems.ToList());

                //Changes the StockQuantity for every Item thats included in the order. 
                order.LineItems = await DecreseStockQuantity(order.LineItems.ToList());

                await _orderRepository.AddAsync(order);

                return _mapper.Map<OrderResponse>(order);
            }
            else
            {
                orderResponse = _mapper.Map<OrderResponse>(orderRequest);
                return orderResponse;
            }
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            var orders = await _orderRepository.ListAllAsync();

            var orderResponse = _mapper.Map<List<OrderResponse>>(orders);

            return orderResponse;
        }

        public async Task<OrderResponse> DeleteOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            await _orderRepository.DeleteAsync(order);
            return _mapper.Map<OrderResponse>(order);

        }

        public async Task<OrderResponse> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            var orderResponse = _mapper.Map<OrderResponse>(order);
            return orderResponse;
        }

        public async Task<OrderResponse> UpdateOrder(OrderRequest orderRequest)
        {
            var orderResponse = new OrderResponse();
            var order = await _orderRepository.GetOrderById(orderRequest.OrderId);

            orderResponse.AddValidationResult(await _validatorFactory.Get("OrderRequestValidator").Validate(orderRequest));
            if (orderResponse.Success)
            {
                order.Status = orderRequest.Status;
                order.LineItems = _mapper.Map<List<LineItem>>(orderRequest.LineItems);
                order.TotalPrice = CalculateTotalOrderPrice(order.LineItems.ToList());

                await _orderRepository.UpdateAsync(order);
            }
            return _mapper.Map<OrderResponse>(order);
        }

        private double CalculateTotalOrderPrice(List<LineItem> lineItems)
        {
            double total = 0; 
            
            lineItems.ForEach(lineItem => 
            {
                var linePrice = lineItem.Item.Price*lineItem.Quantity;
                total+=linePrice;
            });
            return total;
        }
        public async Task<List<LineItem>> DecreseStockQuantity(List<LineItem> lineItems)
        {
            var newQuantityList = new List<LineItem>();
            foreach (var item in lineItems)
            {
                item.Item.StockQuantity -= item.Quantity;
                newQuantityList.Add(item);
            }
            
            return newQuantityList;
        }

        public async Task<List<OrderResponse>> GetOrdersByUser (Guid userId)
        {
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            return _mapper.Map<List<OrderResponse>>(orders);
            
        }
    }
}
