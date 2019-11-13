using BOS.Auth.Client;
using BOS.StarterCode.Controllers;
using BOS.StarterCode.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BOS.StarterCode.Tests
{
    public class PasswordControllerTests
    {
        private readonly IAuthClient _bosAuthClient;

        public PasswordControllerTests()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1YTE1NDNjOC00YzhhLTQ1M2EtOThjNS1kYmE1MTk4NjFlYWYiLCJzdWIiOiJCT1NBcGlLZXkiLCJpYXQiOjE1NzA1NjYwMzcsImFjY291bnQiOiIzMGM3NjkxMC1hYzU5LTRjOWItYmUyZS1kNGQ3OGJmZTBjZDAiLCJwcm9qZWN0IjoiM2RhY2FhZDQtYzE1ZC00NmY3LTk5YjktM2I3NDQ2MjVmYTdiIiwidGVuYW50IjoiNGU1MGNmNDItMzE4MS00N2RmLTk0ZGQtNzE5NTVlNmVmOTY1In0.ebHQCuto1BL3U1_xh8tIJdKqcv9fGMj43icx1edQ0yc");
            httpClient.BaseAddress = new Uri("https://apis.dev.bosframework.com/auth/odata/");

            AuthClient authClient = new AuthClient(httpClient);
            _bosAuthClient = authClient;
        }

        [Fact]
        public async Task ResetPassword_redirects_to_error_view_when_passwordobj_is_null()
        {
            //Arrange
            var controller = new PasswordController(_bosAuthClient);

            //Act
            var result = await controller.ResetPassword(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task ResetPassword_redirects_to_error_view_when_userId_is_null()
        {
            //Arrange
            var controller = new PasswordController(_bosAuthClient);
            ChangePassword passwordObj = new ChangePassword
            {
                CurrentPassword = null,
                NewPassword = "password"
            };

            //Act
            var result = await controller.ResetPassword(passwordObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task ResetPassword_redirects_to_error_view_when_userId_is_incorrect()
        {
            //Arrange
            var controller = new PasswordController(_bosAuthClient);
            ChangePassword passwordObj = new ChangePassword
            {
                CurrentPassword = Guid.NewGuid().ToString(),
                NewPassword = "password"
            };

            //Act
            var result = await controller.ResetPassword(passwordObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public void GotBackToLogin_returns_login_view()
        {
            //Arrange
            var controller = new PasswordController(_bosAuthClient);

            //Act
            var result = controller.GotBackToLogin();

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Auth", redirectResult.ControllerName); //Asseting that the returned Controller is "Auth"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task Reset_redirects_to_error_view_when_slug_is_null()
        {
            //Arrange
            var controller = new PasswordController(_bosAuthClient);

            //Act
            var result = await controller.Reset(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task Reset_redirects_to_error_view_when_slug_is_invalid()
        {
            //Arrange
            var controller = new PasswordController(_bosAuthClient);

            //Act
            var result = await controller.Reset("random-slug-string");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ResetPassword", viewResult.ViewName); //Asseting that the returned Controller is "ResetPassword"
            Assert.True(controller.ViewBag.Message != null); //
        }
    }
}
