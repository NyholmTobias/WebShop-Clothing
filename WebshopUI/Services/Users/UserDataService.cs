using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using Duende.IdentityServer.EntityFramework.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebshopUI.Services.Tokens;

namespace WebshopUI.Services.Users
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenManager _tokenManager;

        public UserDataService(HttpClient httpClient, TokenManager tokenManager)
        {
            _httpClient = httpClient;
            _tokenManager = tokenManager;

        }
        public async Task<List<UserResponse>> GetAllUsers()
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());
            var response = await _httpClient.GetAsync("api/user");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserResponse>>();
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteUser(string email)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            await _httpClient.DeleteAsync($"api/user/{email}");

        }
        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(userRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/user", userJson);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            else
            {
                return null;
            }
        }
        public async Task<UserResponse> UpdateUser(UserRequest userRequest)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var userJson =
                new StringContent(JsonSerializer.Serialize(userRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/user", userJson);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            else
            {
                return null;
            }
        }
        public async Task<UserResponse> GetUserById(string userId)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync($"api/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            else
            {
                return null;
            }


        }

        public async Task AddClaimToUser(UserRequest userRequest, string claimName, string claimValue)
        {
            userRequest.ClaimName = claimName;
            userRequest.ClaimValue = claimValue;
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var userJson =
                new StringContent(JsonSerializer.Serialize(userRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/user/claim", userJson);

        }

        public async Task<List<IdentityUserClaim<string>>> GetUsersClaims(string userId)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync($"api/user/claim/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<IdentityUserClaim<string>>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<UserResponse> GetUserByEmail(string email)
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync($"api/user/email/{email}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            else
            {
                return null;
            }


        }

        public async Task<List<IdentityUserClaim<string>>> GetAllClaims()
        {
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var response = await _httpClient.GetAsync("api/user/getallclaims");

            if (response.IsSuccessStatusCode)
            {
                var identityResources = await response.Content.ReadFromJsonAsync<List<IdentityResource>>();
                return ConvertIdentityResourceToClaims(identityResources);

            }
            else
            {
                return null;
            }
        }

        public async Task AddListOfClaimsToUser(UserRequest userRequest, List<IdentityUserClaim<string>> userClaims)
        {
            userRequest.Claims = userClaims;
            _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

            var userJson =
                new StringContent(JsonSerializer.Serialize(userRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/user/claims", userJson);


        }

        public async Task DeleteListOfClaimsFromUser(UserRequest userRequest)
        {
            foreach (var claim in userRequest.Claims.ToList()) //Lägger till ToList() för att det inte ska crasha om användaren spammar delete knappen
            {
                _httpClient.SetBearerToken(await _tokenManager.RetrieveAccessTokenAsync());

                var response = await _httpClient.DeleteAsync($"api/user/claims/{claim.ClaimType}/{userRequest.Id}");
            }


        }

        private List<IdentityUserClaim<string>> ConvertIdentityResourceToClaims(List<IdentityResource> identityResources)
        {
            var listOfClaims = new List<IdentityUserClaim<string>>();
            foreach (var resource in identityResources)
            {
                var claim = new IdentityUserClaim<string>();
                claim.ClaimType = resource.Name;
                claim.ClaimValue = resource.DisplayName;
                listOfClaims.Add(claim);
            }
            return listOfClaims;
        }
    }
}
