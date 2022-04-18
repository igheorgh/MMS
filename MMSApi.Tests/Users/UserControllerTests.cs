using AutoMapper;
using DataLibrary;
using DataLibrary.DTO;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MMSAPI;
using MMSAPI.Controllers;
using MMSAPI.Models;
using MMSAPI.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MMSApi.Tests.Users
{
    public class UserControllerTests : IClassFixture<TestFixture<Startup>>
    {

        private UserController Controller { get; }

        private List<User> Users = new List<User>();
        private UserDTO TestUser = new UserDTO
        {
            Id = Guid.NewGuid().ToString(),
            Email = "test_user@gmail.com",
            Password = "Password123",
            UserName = "test_user"
        };

        private int NoOfUsers = 6;

        public UserControllerTests(TestFixture<Startup> fixture)
        {
            Users = UserHelpers.GenerateUsers(NoOfUsers);
            var modelUser = TestUser.ToModel();
            modelUser.Roles = "USER,ADMIN";
            Users.Add(modelUser);

            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.Users)
                .Returns(Users.AsQueryable());

            fakeUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>()))
             .ReturnsAsync(IdentityResult.Success);
            fakeUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success).Callback((User user, string pass) => Users.Add(user));
            fakeUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
          .ReturnsAsync(IdentityResult.Success);
            fakeUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
          .ReturnsAsync((string userName) => Users.FirstOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower())));


            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetAll()).Returns(Users);
            userRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns((string id) => Users.FirstOrDefault(u => u.Id == id));

            var dbContext = new Mock<MMSContext>();
            dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            var signInManager = new Mock<FakeSignInManager>();

            signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            Controller = new UserController(dbContext.Object, fakeUserManager.Object, signInManager.Object, null, userRepository.Object);
        }

        [Fact]
        public void GetAllUsers()
        {
            //Act
            var result = (Controller.GetAll() as OkObjectResult).Value as List<UserDTO>;
            // Assert
            Assert.Equal(result.Count(), Users.Count);
        }


        [Fact]
        public async void CreateUser()
        {
            var beforeCount = Users.Count;
            var newUser = new UserDTO
            {
                Email = "test_user2@gmail.com",
                Password = "Password123",
                UserName = "test_user2"
            };
            //Act
            var result = (await Controller.Create(newUser) as OkObjectResult).Value as AuthResponse;
            // Assert
            Assert.Equal(result.email, newUser.Email);
            Assert.Equal(result.username, newUser.UserName);
            Assert.NotNull(result.jwt);
            Assert.Equal(Users.Count, beforeCount + 1);
        }

        private async Task<ObjectResult> GetTestUserLoginData(AuthRequest authRequest = null)
        {
            if (authRequest == null) authRequest = new AuthRequest
            {
                username = TestUser.UserName,
                password = TestUser.Password
            };
            return await Controller.Login(authRequest) as ObjectResult;
        }

        [Fact]
        public async void LoginUserWithInvalidCredentials()
        {
            //Act
            var result = await GetTestUserLoginData(new AuthRequest { username = "aUserNameThatDoesntExist", password = "lalal"});
            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Credentialele nu sunt invalide!", result.Value);
        }

        [Fact]
        public async void LoginUser()
        {
            //Act
            var result = await GetTestUserLoginData();
            // Assert
            Assert.Equal(200, result.StatusCode);

            var authResponse = result.Value as AuthResponse;

            Assert.Equal(TestUser.UserName, authResponse.username);
            Assert.Equal(TestUser.Email, authResponse.email);
            Assert.NotNull(authResponse.jwt);

        }

        [Fact]
        public async void GetMeAsLoggedUser()
        {
            var result = await GetTestUserLoginData();
            Assert.Equal(200, result.StatusCode);
            var authResponse = result.Value as AuthResponse;

            var token = new JwtSecurityTokenHandler().ReadJwtToken(authResponse.jwt);
            var identity = new ClaimsPrincipal(new ClaimsIdentity(token.Claims));

            Controller.ControllerContext = new ControllerContext();
            Controller.ControllerContext.HttpContext = new DefaultHttpContext { User = identity };

            //Act
            var meUser = (await Controller.GetMe() as OkObjectResult).Value as UserDTO;
            // Assert
            Assert.Equal(TestUser.Id, meUser.Id);
        }
    }
}