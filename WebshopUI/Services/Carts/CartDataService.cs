using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabBlazor.Services;
using WebshopCache.Factories;
using WebshopCache.Services;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Orders;
using WebshopUI.Services.Users;

namespace WebshopUI.Services.Carts
{
    public class CartDataService : ComponentBase, ICartDataService
    {
        private readonly ICacheFactory _cacheFactory;
        private readonly ICacheService _cacheService;
        private readonly IOrderDataService _orderDataService;
        private readonly IUserDataService _userDataService;
        private readonly AuthenticationStateProvider AuthenticationStateProvider;
        private readonly IMapper _mapper;


        public CartDataService(ICacheFactory cacheFactory, ICacheService cacheService, IOrderDataService orderDataService, IMapper mapper, IUserDataService userDataService, AuthenticationStateProvider authenticationStateProvider)
        {
            _cacheFactory = cacheFactory;
            _cacheService = cacheService;
            _orderDataService = orderDataService;
            _mapper = mapper;
            _userDataService=userDataService;
            AuthenticationStateProvider=authenticationStateProvider;
        }

        public async Task AddToCart(LineItemResponse lineÍtem, Guid userId)
        {
            var listOfLineItems = new List<LineItemResponse>();
            
            //Hämta rätt cache ifrån factory
            var cache = _cacheFactory.GetCache(WebshopCache.CacheName.Carts);
            var cartFromCache = await cache.GetCacheAsync(userId);
            //Om cachen inte är tom
            if (cartFromCache != null)
            {
                //Gör om byte array till list av line items
                listOfLineItems = _cacheService.DeserialiseToListOfLineItems(cartFromCache);
            }
            //lägg till nya itemet
            listOfLineItems.Add(CreateLineItemForCart(lineÍtem));
            //Spara i cachen
            await cache.SetCacheAsync(_cacheService.SerialiseListOfLineItemsToBytes(listOfLineItems), userId);
        }

        public async Task RemoveFromCart(LineItemResponse lineItem, Guid userId)
        {
            var listOfLineItems = new List<LineItemResponse>();

            //Hämta rätt cache ifrån factory
            var cache = _cacheFactory.GetCache(WebshopCache.CacheName.Carts);
            var cartFromCache = await cache.GetCacheAsync(userId);

            //Om cachen inte är tom
            if (cartFromCache != null)
            {
                //Gör om byte array till list av line items
                listOfLineItems = _cacheService.DeserialiseToListOfLineItems(cartFromCache);
            }
            //Tar bort item med matchande itemID
            listOfLineItems.Remove(listOfLineItems.FirstOrDefault(lineItem => lineItem.ItemId == lineItem.ItemId));

            //Spara i cachen
            await cache.SetCacheAsync(_cacheService.SerialiseListOfLineItemsToBytes(listOfLineItems), userId);
        }

        public async Task ClearCart(Guid userId)
        {
            var listOfLineItems = new List<LineItemResponse>();
            //Hämta rätt cache ifrån factory
            var cache = _cacheFactory.GetCache(WebshopCache.CacheName.Carts);
            //Spara en tom lista i cachen
            await cache.SetCacheAsync(_cacheService.SerialiseListOfLineItemsToBytes(listOfLineItems), userId);
        }

        public async Task<OrderResponse> CreateOrderFromCart(Guid userId)
        {
            //Hämtar ut rätt user
            var identity = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await _userDataService.GetUserByEmail(identity.User.Identity.Name);
            //Hämta rätt cache ifrån factory
            var cache = _cacheFactory.GetCache(WebshopCache.CacheName.Carts);
            var cartFromCache = await cache.GetCacheAsync(userId);
            var listOfLineItems = _cacheService.DeserialiseToListOfLineItems(cartFromCache);

            var order = new OrderResponse
            {
                UserId = userId,
                Username = user.UserName,
                LineItems = listOfLineItems,
            };

             return await _orderDataService.CreateOrder(_mapper.Map<OrderRequest>(order));
            
        }

        public async Task<List<LineItemResponse>> GetShoppingCart(Guid userId)
            {
            var cache = _cacheFactory.GetCache(WebshopCache.CacheName.Carts);
            var cartFromCache = await cache.GetCacheAsync(userId);
            if (cartFromCache == null)
            {
                var list = new List<LineItemResponse>()
                {
                    new LineItemResponse { Item = new ItemResponse { Description = "Cart is empty, why wont you buy my face?" } }
                };
                return list;
            }
            return _cacheService.DeserialiseToListOfLineItems(cartFromCache);
        }

        private LineItemResponse CreateLineItemForCart(LineItemResponse lineItem)
        {
            return lineItem;
        }

        
    }
}
