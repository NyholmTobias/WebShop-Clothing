using AutoMapper;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;
using WebshopUI.Services.Tokens;

namespace WebshopUI.Services.Orders
{
    public class OrderDataService : IOrderDataService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenManager _tokenManager;
        private readonly IMapper _mapper;

        public OrderDataService(HttpClient httpClient, TokenManager tokenManager, IMapper mapper)
        {
            _httpClient = httpClient;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }

        public async Task<OrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var orderJson =
                new StringContent(JsonSerializer.Serialize(orderRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/order/create", orderJson);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            else
            {
                var order = await response.Content.ReadFromJsonAsync<OrderResponse>();
                
                    order.Success = false;
                    if (order.ValidationErrors == null)
                    {
                        order.ValidationErrors = new List<string>();
                    order.ValidationErrors.Add("Something went wrong");
                    }
                return order;



            }
            
            
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync("api/v1/order");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
            }
            else
            {
                var orders = await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
                if (orders.Count <= 0)
                {
                    orders.Add(new OrderResponse { });
                }

                foreach (var order in orders)
                {
                    order.Success = false;
                    if (order.ValidationErrors == null)
                    {

                        order.ValidationErrors = new List<string>();
                        order.ValidationErrors.Add("Something went wrong");
                    }
                    
                }
                return orders;
            }
        }
        
        public async Task<OrderResponse> GetOrderById(Guid orderId)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync($"api/v1/order/{orderId}");


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            else
            {

                var orderReponse = await response.Content.ReadFromJsonAsync<OrderResponse>();
                orderReponse.Success = false;
                if (orderReponse.ValidationErrors == null)
                {
                    orderReponse.ValidationErrors = new List<string>();
                    orderReponse.ValidationErrors.Add("Something went wrong");
                }
                return orderReponse;
            }
        }

        public async Task<OrderResponse> DeleteOrder(Guid orderId)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.DeleteAsync($"api/v1/order/{orderId}");


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            else
            {
                var orderReponse = await response.Content.ReadFromJsonAsync<OrderResponse>();
                orderReponse.Success = false;
                if (orderReponse.ValidationErrors == null)
                {
                    orderReponse.ValidationErrors = new List<string>();
                    orderReponse.ValidationErrors.Add("Something went wrong");
                }
                return orderReponse;
            }
        }

        public async Task<OrderResponse> UpdateOrder(OrderRequest orderRequest)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var orderJson =
               new StringContent(JsonSerializer.Serialize(orderRequest), Encoding.UTF8, "application/json");

           var response = await _httpClient.PutAsync($"api/v1/order", orderJson);

           
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            else
            {

                var orderReponse = await response.Content.ReadFromJsonAsync<OrderResponse>();
                orderReponse.Success = false;
                if (orderReponse.ValidationErrors == null)
                {
                    orderReponse.ValidationErrors = new List<string>();
                    orderReponse.ValidationErrors.Add("Something went wrong");
                }
                return orderReponse;
            }
        }

        public async Task<List<OrderResponse>> GetAllOrdersByUser(Guid userId)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync($"api/v1/order/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
            }
            else
            {
                var orders = await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
                if (orders.Count <= 0)
                {
                    orders.Add(new OrderResponse { });
                }

                foreach (var order in orders)
                {
                    order.Success = false;
                    if (order.ValidationErrors == null)
                    {
                        order.ValidationErrors = new List<string>();
                        order.ValidationErrors.Add("Something went wrong");
                    }
                    
                }
                return orders;
            }
        }
    }
}
