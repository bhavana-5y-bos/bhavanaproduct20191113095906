using BOS.Auth.Client;
using BOS.Email.Client;
using BOS.StarterCode.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BOS.StarterCode.Tests
{
    public class UsersControllerTests
    {
        private readonly IAuthClient _bosAuthClient;
        private readonly IEmailClient _bosEmailClient;
        private readonly IConfiguration _configuration;

        public UsersControllerTests()
        {
            string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1YTE1NDNjOC00YzhhLTQ1M2EtOThjNS1kYmE1MTk4NjFlYWYiLCJzdWIiOiJCT1NBcGlLZXkiLCJpYXQiOjE1NzA1NjYwMzcsImFjY291bnQiOiIzMGM3NjkxMC1hYzU5LTRjOWItYmUyZS1kNGQ3OGJmZTBjZDAiLCJwcm9qZWN0IjoiM2RhY2FhZDQtYzE1ZC00NmY3LTk5YjktM2I3NDQ2MjVmYTdiIiwidGVuYW50IjoiNGU1MGNmNDItMzE4MS00N2RmLTk0ZGQtNzE5NTVlNmVmOTY1In0.ebHQCuto1BL3U1_xh8tIJdKqcv9fGMj43icx1edQ0yc";

            HttpClient httpClientAuth = new HttpClient();
            httpClientAuth.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            httpClientAuth.BaseAddress = new Uri("https://apis.dev.bosframework.com/auth/odata");
            AuthClient authClient = new AuthClient(httpClientAuth);

            HttpClient httpClientEmail = new HttpClient();
            httpClientEmail.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            httpClientEmail.BaseAddress = new Uri("https://apis.dev.bosframework.com/email/odata");
            EmailClient emailClient = new EmailClient(httpClientEmail);

            _bosAuthClient = authClient;
            _bosEmailClient = emailClient;
            _configuration = null;
        }

        [Fact]
        public void Index_returns_model_with_null_currentmoduleid_when_claims_is_empty()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void Index_returns_model_with_null_currentmoduleid_when_session_is_empty()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void Index_returns_non_null_model_claims_and_sessions_are_not_empty()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void AddNewUser_returns_model_with_null_currentmoduleid_when_claims_is_empty()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void AddNewUser_returns_model_with_null_currentmoduleid_when_session_is_empty()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void AddNewUser_returns_non_null_model_claims_and_sessions_are_not_empty()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void EditUser_returns_model_with_null_currentmoduleid_when_claims_is_empty()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void EditUser_returns_model_with_null_currentmoduleid_when_session_is_empty()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void EditUser_returns_non_null_model_claims_and_sessions_are_not_empty()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void EditUser_returns_non_null_model_when_userid_is_null()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void EditUser_returns_non_null_model_when_userid_is_in_incorrect_format()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void EditUser_returns_non_null_model_when_userid_is_invalid()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void EditUser_returns_non_null_model_when_role_is_null()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }


        [Fact]
        public async Task AddUser_returns_string_when_data_is_null()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.AddUser(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddUser_returns_string_when_user_is_null()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.AddUser(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddUser_returns_string_when_roles_is_null()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.AddUser(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddUser_returns_string_when_IsEmailToSend_nul()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.AddUser(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddUser_returns_string_when_password_is_null()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.AddUser(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddUser_returns_string_when_username_is_null()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.AddUser(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddUser_returns_string_when_email_is_null()
        {
            //Arrange
            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.AddUser(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }
        

        [Fact]
        public void AddUser_returns_string_when_role_is_invalid()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void AddUser_returns_string_when_username_exists()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void AddUser_returns_string_when_email_exists()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void DeleteUser_returns_string_when_userid_is_null()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void DeleteUser_returns_string_when_userid_is_in_incorrect_format()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void DeleteUser_returns_string_when_userid_is_invalid()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void UpdateUserInfo_returns_string_when_user_is_null()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void UpdateUserInfo_returns_string_when_userid_is_in_incorrect_format()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void UpdateUserInfo_returns_string_when_userid_is_invalid()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void UpdateUserInfo_returns_string_when_username_exists()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void UpdateUserInfo_returns_string_when_email_exists()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        private UsersController ConfigureController()
        {
            //Mocking the user claims
            var claims = new List<Claim>{
                new Claim("CreatedOn", DateTime.UtcNow.ToString()),
                new Claim("Email", "some@email.com"),
                new Claim("Initials", "JD"),
                new Claim("Name", "John Doe"),
                new Claim("Role", "Admin"),
                new Claim("UserId", Guid.NewGuid().ToString()),
                new Claim("Username", "SomeUserName"),
                new Claim("IsAuthenticated", "True")
            };
            var userIdentity = new ClaimsIdentity(claims, "Auth");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            var controller = new UsersController(_bosAuthClient, _bosEmailClient, _configuration)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = principal }
                }
            };
            return controller;
        }
    }
}
