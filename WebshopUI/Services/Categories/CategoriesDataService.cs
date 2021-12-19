using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Services.Tokens;

namespace WebshopUI.Services.Categories
{
    public class CategoriesDataService : ICategoriesDataService
    {
        private readonly HttpClient _httpClient;
        //private readonly TokenManager _tokenManager;
        public CategoriesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            //Kommenterade ut då denna ska kunna nås utan att vara inloggad
            //_httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());
            var response = await _httpClient.GetAsync("api/v1/category");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            else
            {
                var orders = await response.Content.ReadFromJsonAsync<List<CategoryResponse>>();
                if (orders.Count <= 0)
                {
                    orders.Add(new CategoryResponse { });
                }

                return orders;
            }
        }

        public async Task<CategoryResponse> GetCategoryByName(string categoryName)
        {
            //Kommenterade ut då denna ska kunna nås utan att vara inloggad
            //_httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());
            var response = await _httpClient.GetAsync($"api/v1/category/name/{categoryName}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryResponse>();
            }
            else
            {
                var order = await response.Content.ReadFromJsonAsync<CategoryResponse>();
                if (order.ValidationErrors.Count == 0)
                {
                    order.ValidationErrors.Add("Could not find category!");
                }

                return order;
            }
        }
    }
}
