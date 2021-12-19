using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Services.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();

        Task<UserResponse> DeleteUser(string email);
        
        Task<UserResponse> CreateUser(UserRequest userRequest);
        
        Task<UserResponse> UpdateUser(UserRequest userRequest);
        
        Task<UserResponse> GetUserById(string userId);
        
        
        Task<List<IdentityUserClaim<string>>> GetUsersClaims(string userId);
        
        Task<UserResponse> GetUserByEmail(string email);
        
        Task<List<IdentityUserClaim<string>>> AddListOfClaimsToUser(UserRequest user);
        
        Task DeleteClaimFromUser(string claimType, string userId);
        
        Task<List<IdentityResource>> GetAllClaims();
        
    }
}
