using Microsoft.AspNetCore.Mvc;
using PlaceAgregator.API.Controllers;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.Shared.DTOs;
using PlaceAgregator.Shared.DTOs.Authentication;
using PlaceAgregator.Shared.Models.Enums;
using Moq;
using Xunit;

namespace PlaceAgregator.UnitTests
{
    public class AuthControllerTests
    {
        [Fact]
        public void LoginAsync_WithUserAndPassword_ReturnedOkObjectResult()
        {
            string login = "User";
            string password = "Password";
            LoginRequest loginRequest = new LoginRequest()
            {
                UserNameOrEmail = login,
                Password = password
            };

            var mock = new Mock<IAuthService>();
            mock.Setup((item) => item.LoginAsync(login, password))
                .ReturnsAsync(() => new Response<LoginResponse>()
                {
                    Message = new LoginResponse("afdsfsdf", login, new string[] { RoleConstants.User }),
                    Succeeded = true
                });

            AuthController authController = new AuthController(mock.Object);
            IActionResult actionResult = authController.LoginAsync(loginRequest).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void LoginAsync_WithoutUsername_ReturnedBadRequestResult()
        {
            string login = "User";
            string password = "Password";
            LoginRequest loginRequest = new LoginRequest()
            {
                Password = password
            };

            var mock = new Mock<IAuthService>();
            mock.Setup((item) => item.LoginAsync(login, password))
                .ReturnsAsync(() => new Response<LoginResponse>()
                {
                    Message = new LoginResponse("afdsfsdf", login, new string[] { RoleConstants.User }),
                    Succeeded = true
                });

            AuthController authController = new AuthController(mock.Object);
            IActionResult actionResult = authController.LoginAsync(loginRequest).GetAwaiter().GetResult();

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void LoginAsync_WithoutPassword_ReturnedBadRequestResult()
        {
            string login = "User";
            string password = "Password";
            LoginRequest loginRequest = new LoginRequest()
            {
                UserNameOrEmail = login
            };

            var mock = new Mock<IAuthService>();
            mock.Setup((item) => item.LoginAsync(login, password))
                .ReturnsAsync(() => new Response<LoginResponse>()
                {
                    Message = new LoginResponse("afdsfsdf", login, new string[] { RoleConstants.User }),
                    Succeeded = true
                });

            AuthController authController = new AuthController(mock.Object);
            IActionResult actionResult = authController.LoginAsync(loginRequest).GetAwaiter().GetResult();

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void LoginAsync_BadAuthtorization_ReturnedBadRequestObjectResult()
        {
            string login = "User";
            string password = "Password";
            LoginRequest loginRequest = new LoginRequest()
            {
                UserNameOrEmail = login,
                Password = password
            };

            var mock = new Mock<IAuthService>();
            mock.Setup((item) => item.LoginAsync(login, password))
                .ReturnsAsync(() => new Response<LoginResponse>()
                {
                    Errors = new ResponseError[] { new("PasswordInvalid", "Пароль неверный") },
                    Succeeded = false
                });

            AuthController authController = new AuthController(mock.Object);
            IActionResult actionResult = authController.LoginAsync(loginRequest).GetAwaiter().GetResult();

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
    }
}
