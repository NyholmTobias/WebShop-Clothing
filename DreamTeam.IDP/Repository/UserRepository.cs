using DreamTeam.IDP.Data;
using DreamTeam.IDP.Shared.Models;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ConfigurationDbContext _configurationDbContext;
        public UserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, ConfigurationDbContext configurationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _configurationDbContext = configurationDbContext;
        }
        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Returns list of all users
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<ApplicationUser>> ListAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task AddClaimToUserAsync(ApplicationUser user, Claim claim)
        {
            await _userManager.AddClaimAsync(user, claim);
        }


        public async Task DeleteUserAsync(ApplicationUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
            return await _userManager.FindByEmailAsync(user.Email);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            var foundUser = await _userManager.FindByIdAsync(user.Id);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
                foundUser.PhoneNumber = user.PhoneNumber;
                foundUser.UserName = user.UserName;
            }
            await _userManager.UpdateAsync(foundUser);
        }
        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;

        }

        public async Task<List<IdentityUserClaim<string>>> GetClaimsByUser(ApplicationUser user)
        {
            var claims = await _applicationDbContext.UserClaims.Where(claims => claims.UserId == user.Id).ToListAsync();

            return claims;
        }

        /// <summary>
        /// Gets all available claims from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdentityResource>> GetAllClaims()
        {
            var test = await _configurationDbContext.IdentityResources.ToListAsync();
            return test;
        }

        public async Task<List<IdentityUserClaim<string>>> AddListOfClaimsToUser(List<IdentityUserClaim<string>> claims)
        {
            _applicationDbContext.UserClaims.AddRange(claims);
            await _applicationDbContext.SaveChangesAsync();
            return claims;
        }
        public async Task DeleteClaimFromUser(IdentityUserClaim<string> claim)
        {
            _applicationDbContext.UserClaims.Remove(claim);
            await _applicationDbContext.SaveChangesAsync();
        }

    }
}
