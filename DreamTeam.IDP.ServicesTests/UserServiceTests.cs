using AutoMapper;
using DreamTeam.IDP.Repository;
using DreamTeam.IDP.Services;
using DreamTeam.IDP.Services.Services;
using DreamTeam.IDP.Shared.Models;
using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace DreamTeam.IDP.ServicesTests
{
    public class UserServiceTests
    {
        UserTestHelper _testHelper = new UserTestHelper();
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public UserServiceTests()
        {
            
            _mockUserRepository = new Mock<IUserRepository>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
        [Fact]
        public async void ShouldReturnListOfUserResponses()
        {
            //Arrange
            var users = _testHelper.CreateListOfThreeApplicationUsers();
            _mockUserRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(users);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.GetAllUsersAsync();
            //Assert
            result.ShouldBeOfType<List<UserResponse>>();
        }

        [Fact]
        public async void ShouldReturnListOf3Users()
        {
            //Arrange
            var users = _testHelper.CreateListOfThreeApplicationUsers();
            _mockUserRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(users);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.GetAllUsersAsync();
            //Assert
            result.Count.ShouldBe(3);

        }

        [Fact]
        public async void DeleteShouldCallGetByEmailOnce()
        {
            //Arrange
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.DeleteUser(user.Email);
            //Assert
            _mockUserRepository.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async void DeleteShouldCalDeleteUserAsyncOnce()
        {
            //Arrange
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.DeleteUser(user.Email);
            //Assert
            _mockUserRepository.Verify(x => x.DeleteUserAsync(user), Times.Once());
        }

        [Fact]
        public async void CreateUserShouldReturnUserResponse()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.CreateUserAsync(user, It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.AddClaimToUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>()));
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.CreateUser(userRequest);
            //Assert
            result.ShouldBeOfType<UserResponse>();


        }

        [Fact]
        public async void CreateUserShouldCallAddClaimsAtleastOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.CreateUserAsync(user, It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.AddClaimToUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>()));
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.CreateUser(userRequest);
            //Assert
            _mockUserRepository.Verify(repo => repo.AddClaimToUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>()), Times.AtLeastOnce());

        }

        [Fact]
        public async void CreateUserShouldCallGetEmailOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.CreateUserAsync(user, It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.AddClaimToUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>()));
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.CreateUser(userRequest);
            //Assert
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public async void UpdateUserShouldCallUpdateAsyncOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.UpdateUser(userRequest);
            //Assert
            _mockUserRepository.Verify(repo => repo.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Once());

        }
        [Fact]
        public async void UpdateUserShouldCallGetByIdOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.UpdateUser(userRequest);
            //Assert
            _mockUserRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public async void GetUserByIdShouldCallGetByIdOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.GetUserById(user.Id);
            //Assert
            _mockUserRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public async void GetUserByIdShouldReturnUserResponse()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.UpdateUser(userRequest);
            //Assert
            result.ShouldBeOfType<UserResponse>();
        }

        [Fact]
        public async void GetUsersClaimsShouldCallGetUserClaimsOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            var claims = new List<IdentityUserClaim<string>>();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            await sut.GetUsersClaims(user.Id);
            //Assert
            _mockUserRepository.Verify(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>()), Times.Once());
        }

        [Fact]
        public async void GetUsersClaimsShouldReturnListOFIdentityClaims()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            var claims = new List<IdentityUserClaim<string>>();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.GetUsersClaims(user.Id);
            //Assert
            result.ShouldBeOfType<List<IdentityUserClaim<string>>>();
        }

        [Fact]
        public async void GetUsersClaimsShouldCallGetByIdOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            var claims = new List<IdentityUserClaim<string>>();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.GetUsersClaims(user.Id);
            //Assert
            _mockUserRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async void GetUserByEmailShouldCallGetByEmailOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.GetUserByEmail(user.Email);
            //Assert
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async void AddListOfClaimsShouldCallAddClaimsOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            var claims = new List<IdentityUserClaim<string>>();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            await sut.AddListOfClaimsToUser(userRequest);
            //Assert
            _mockUserRepository.Verify(repo => repo.AddListOfClaimsToUser(It.IsAny<List<IdentityUserClaim<string>>>()), Times.Once());
        }

        [Fact]
        public async void AddListOfClaimsShouldCallGetClaimsByUserOnce()
        {
            //Arrange
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            var claims = new List<IdentityUserClaim<string>>();
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            await sut.AddListOfClaimsToUser(userRequest);
            //Assert
            _mockUserRepository.Verify(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>()), Times.Once());
        }

        [Fact]
        public async void AddListOfClaimsShouldReturnListOfOneClaim()
        {
            
            //Arrange
            var existingClaims = _testHelper.CreateExistingClaims();
            var claims = _testHelper.CreateListOfClaims();
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();
            userRequest.Claims = claims;
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>())).ReturnsAsync(existingClaims);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            var result = await sut.AddListOfClaimsToUser(userRequest);
            //Assert
            result.Count.ShouldBe(1);
        }

        [Fact]
        public async void DeleteClaimFromUserShouldCallDeleteOnce()
        {
            //Arrange
            var existingClaims = _testHelper.CreateExistingClaims();
            var claims = _testHelper.CreateListOfClaims();
            var userRequest = _testHelper.CreateUserRequest();
            var user = _testHelper.CreateApplicationUser();

            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.GetClaimsByUser(It.IsAny<ApplicationUser>())).ReturnsAsync(existingClaims);
            //Act
            var sut = new UserService(_mockUserRepository.Object, _mapper);
            await sut.DeleteClaimFromUser("ExistingType", userRequest.Id);
            //Assert
            _mockUserRepository.Verify(repo => repo.DeleteClaimFromUser(It.IsAny<IdentityUserClaim<string>>()), Times.Once());
        }


    }
}