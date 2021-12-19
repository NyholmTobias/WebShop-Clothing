using DreamTeam.IDP.Shared.Models;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Repository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email);

        Task<IReadOnlyList<ApplicationUser>> ListAllAsync();


        Task AddClaimToUserAsync(ApplicationUser user, Claim claim);

        Task DeleteUserAsync(ApplicationUser user);

        Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password);

        Task UpdateUserAsync(ApplicationUser user);

        Task<ApplicationUser> GetByIdAsync(string id);


        Task<List<IdentityUserClaim<string>>> GetClaimsByUser(ApplicationUser user);

        Task<List<IdentityResource>> GetAllClaims();

        Task<List<IdentityUserClaim<string>>> AddListOfClaimsToUser(List<IdentityUserClaim<string>> claims);

        Task DeleteClaimFromUser(IdentityUserClaim<string> claim);
        
    }
        
}
