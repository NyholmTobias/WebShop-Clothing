using DreamTeam.IDP.Shared.Models;
using DreamTeam.IDP.Shared.RequestModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.ServicesTests
{
    public class UserTestHelper
    {
        public UserTestHelper()
        {

        }

        public List<ApplicationUser> CreateListOfThreeApplicationUsers()
        {
            var guid1 = Guid.Parse("{B0788D2F-8093-43C1-92A4-EDC76A7C5DDE}");
            var guid2 = Guid.Parse("{B0788D2F-8903-43C1-92A4-EDC76A7C5DDE}");
            var guid3 = Guid.Parse("{B0788D2F-8803-43C1-92A4-EDC76A7C5DDE}");
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser { Id = guid1.ToString(), UserName = "testuser"},
                new ApplicationUser { Id = guid2.ToString(), UserName = "testuser2"},
                new ApplicationUser { Id = guid3.ToString(), UserName = "testuser3"}
            };
            return users;
        }

        public ApplicationUser CreateApplicationUser()
        {
            var user = new ApplicationUser()
            {
                UserName = "TestUser",
                Email = "testemail",
                Id = "testId"
            };
            return user; 
        }

        public UserRequest CreateUserRequest()
        {
            var userRequest = new UserRequest()
            {
                UserName = "TestUser",
                Email = "testemail",
                Id = "testId",
                Claims = new List<IdentityUserClaim<string>>()
            };
            return userRequest;
        }

        public List<IdentityUserClaim<string>> CreateListOfClaims()
        {
            var claims = new List<IdentityUserClaim<string>>()
            {
                new IdentityUserClaim<string> { ClaimType = "ExistingType"},
                new IdentityUserClaim<string> { ClaimType = "NonExistingType"}
            };

            return claims;
        }

        public List<IdentityUserClaim<string>> CreateExistingClaims()
        {
            var existingClaims = new List<IdentityUserClaim<string>>()
            {
                new IdentityUserClaim<string> { ClaimType = "ExistingType"}

            };

            return existingClaims;
        }
    }
}
