using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebshopUI.Services.Users
{
    public interface IUserDataService
    {
        Task<List<UserResponse>> GetAllUsers();


        Task DeleteUser(string email);

        Task<UserResponse> CreateUser(UserRequest userRequest);

        Task<UserResponse> UpdateUser(UserRequest userRequest);

        Task<UserResponse> GetUserById(string userId);


        Task AddClaimToUser(UserRequest userRequest, string claimName, string claimValue);


        Task<List<IdentityUserClaim<string>>> GetUsersClaims(string userId);


        Task<UserResponse> GetUserByEmail(string email);


        Task<List<IdentityUserClaim<string>>> GetAllClaims();

        Task AddListOfClaimsToUser(UserRequest userRequest, List<IdentityUserClaim<string>> userClaims);


        Task DeleteListOfClaimsFromUser(UserRequest userRequest);
        

    }
}
