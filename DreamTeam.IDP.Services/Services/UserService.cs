using AutoMapper;
using DreamTeam.IDP.Repository;
using DreamTeam.IDP.Shared.Models;
using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper=mapper;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.ListAllAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }

        public async Task<UserResponse> DeleteUser(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                await _userRepository.DeleteUserAsync(user);
                return _mapper.Map<UserResponse>(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            var user = await _userRepository.GetByEmailAsync(userRequest.Email);
            if (user == null)
            {

                var newUser = _mapper.Map<ApplicationUser>(userRequest);
                newUser.EmailConfirmed = true;
                newUser.UserID = Guid.NewGuid();
                await _userRepository.CreateUserAsync(newUser, userRequest.Password);

                await _userRepository.AddClaimToUserAsync(newUser, new Claim("email", userRequest.Email));
                await _userRepository.AddClaimToUserAsync(newUser, new Claim("username", userRequest.UserName));
                await _userRepository.AddClaimToUserAsync(newUser, new Claim("customer", "customer"));
                return _mapper.Map<UserResponse>(newUser);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserResponse> UpdateUser(UserRequest userRequest)
        {
            var user = await _userRepository.GetByIdAsync(userRequest.Id);

            if (user != null)
            {
                var userToBeUpdated = _mapper.Map<ApplicationUser>(userRequest);
                await _userRepository.UpdateUserAsync(userToBeUpdated);
                return _mapper.Map<UserResponse>(userToBeUpdated);
            }
            else
            {
                return null;
            }
        }
        public async Task<UserResponse> GetUserById(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                return _mapper.Map<UserResponse>(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<IdentityUserClaim<string>>> GetUsersClaims(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return await _userRepository.GetClaimsByUser(user);
        }

        public async Task<UserResponse> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            return _mapper.Map<UserResponse>(user);
        }



        public async Task<List<IdentityUserClaim<string>>> AddListOfClaimsToUser(UserRequest user)
        {
            var claimsToBeAdded = new List<IdentityUserClaim<string>>();

            var userFromDb = await _userRepository.GetByEmailAsync(user.Email);
            var userClaimsFromDb = await _userRepository.GetClaimsByUser(userFromDb);

            foreach (var claim in user.Claims)
            {
                claim.UserId = userFromDb.Id;
                if (!userClaimsFromDb.Any(claims => claims.ClaimType == claim.ClaimType))
                {
                    claimsToBeAdded.Add(claim);
                }
            }

            await _userRepository.AddListOfClaimsToUser(claimsToBeAdded);

            return claimsToBeAdded;
        }

        public async Task DeleteClaimFromUser(string claimType, string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var claimsFromDb = await _userRepository.GetClaimsByUser(user);

            var claimToBeDeleted = claimsFromDb.FirstOrDefault(x => x.ClaimType == claimType);

            await _userRepository.DeleteClaimFromUser(claimToBeDeleted);
        }

        public async Task<List<IdentityResource>> GetAllClaims()
        {
            var listOfClaims = await _userRepository.GetAllClaims();
            return listOfClaims;
        }

    }
}

