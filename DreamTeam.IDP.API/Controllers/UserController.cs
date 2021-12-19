
using DreamTeam.IDP.Services.Services;
using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamTeam.IDP.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
        {
            var userResponses = await _userService.GetAllUsersAsync();
            if (userResponses != null)
            {
                return Ok(userResponses);
            }
            else
            {
                return BadRequest(userResponses);
            }
        }
        [HttpDelete("{email}")]
        public async Task<ActionResult<UserResponse>> DeleteUser(string email)
        {
            var userResponse = await _userService.DeleteUser(email);
            if (userResponse != null)
            {
                return Ok(userResponse);
            }
            else
            {
                return BadRequest(userResponse);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser(UserRequest userRequest)
        {
            var userResponse = await _userService.CreateUser(userRequest);
            if (userResponse != null)
            {
                return Ok(userResponse);
            }
            else
            {
                return BadRequest(userResponse);
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserResponse>> UpdateUser(UserRequest userRequest)
        {
            var userResponse = await _userService.UpdateUser(userRequest);
            if (userResponse != null)
            {
                return Ok(userResponse);
            }
            else
            {
                return BadRequest(userResponse);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponse>> GetUserById(string userId)
        {
            var userResponse = await _userService.GetUserById(userId);
            if (userResponse != null)
            {
                return Ok(userResponse);
            }
            else
            {
                return BadRequest(userResponse);
            }
        }
       
        [HttpGet("claim/{userId}")]
        public async Task<ActionResult<List<IdentityUserClaim<string>>>> GetUsersClaims(string userId)
        {
            var claims = await _userService.GetUsersClaims(userId);
            if (claims != null)
            {
                return Ok(claims);
            }
            else
            {
                return BadRequest(claims);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserResponse>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(user);
            }
        }


        [HttpPost("claims")]
        public async Task<ActionResult<List<IdentityUserClaim<string>>>> AddListOfClaimsToUser(UserRequest user)
        {
            var response = await _userService.AddListOfClaimsToUser(user);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("claims/{claimType}/{Id}")]
        public async Task DeleteClaimFromUser(string claimType, string Id)
        {
            await _userService.DeleteClaimFromUser(claimType, Id);
        }

        [HttpGet("getallclaims/")]
        public async Task<ActionResult<List<IdentityResource>>> GetAllClaims()
        {
            var claims = await _userService.GetAllClaims();
            if (claims != null)
            {
                return Ok(claims);
            }
            else
            {
                return BadRequest(claims);
            }
        }

    }
}
