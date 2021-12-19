using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;
using WebshopUI.Services.Tokens;

namespace WebshopUI.Services.Items
{
    public class ItemDataService : IItemDataService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenManager _tokenManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public ItemDataService(HttpClient httpClient, TokenManager tokenManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _tokenManager = tokenManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<List<ItemResponse>> GetAllItems()
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync("api/v1/Item");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ItemResponse>>();
            }
            else
            {
                var itemResponse = await response.Content.ReadFromJsonAsync<List<ItemResponse>>();
                if (itemResponse.Count <= 0)
                {
                    itemResponse.Add(new ItemResponse { });
                }

                return itemResponse;
            }
        }

        public async Task<ItemResponse> UpdateItem(ItemRequest itemRequest)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var itemJson =
               new StringContent(JsonSerializer.Serialize(itemRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/v1/Item", itemJson);


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ItemResponse>();
            }
            else
            {

                var orderReponse = await response.Content.ReadFromJsonAsync<ItemResponse>();
                orderReponse.Success = false;
                if (orderReponse.ValidationErrors == null)
                {
                    orderReponse.ValidationErrors = new List<string>();
                    orderReponse.ValidationErrors.Add("Something went wrong");
                }
                return orderReponse;
            }
        }
    }
}
