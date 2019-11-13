using BOS.IA.Client;
using BOS.StarterCode.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BOS.StarterCode.Tests
{
    public class PermissionsControllerTests
    {
        private readonly IIAClient _bosIAClient;

        public PermissionsControllerTests()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1YTE1NDNjOC00YzhhLTQ1M2EtOThjNS1kYmE1MTk4NjFlYWYiLCJzdWIiOiJCT1NBcGlLZXkiLCJpYXQiOjE1NzA1NjYwMzcsImFjY291bnQiOiIzMGM3NjkxMC1hYzU5LTRjOWItYmUyZS1kNGQ3OGJmZTBjZDAiLCJwcm9qZWN0IjoiM2RhY2FhZDQtYzE1ZC00NmY3LTk5YjktM2I3NDQ2MjVmYTdiIiwidGVuYW50IjoiNGU1MGNmNDItMzE4MS00N2RmLTk0ZGQtNzE5NTVlNmVmOTY1In0.ebHQCuto1BL3U1_xh8tIJdKqcv9fGMj43icx1edQ0yc");
            httpClient.BaseAddress = new Uri("https://apis.dev.bosframework.com/auth/odata/");

            IAClient iaClient = new IAClient(httpClient);
            _bosIAClient = iaClient;
        }

        [Fact]
        public void FetchPermissions_returns_model_with_null_currentmoduleid_when_session_is_empty()
        {
            //Arrange
            var controller = new PermissionsController(_bosIAClient);

            //Act
            var result = controller.FetchPermissions(null, null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void FetchPermissions_returns_non_null_model_claims_and_sessions_are_not_empty()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.FetchPermissions(null, null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void FetchPermissions_redirects_to_error_when_roleid_is_null()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.FetchPermissions(null, "roleName");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);

            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned Controller is "Home"
            Assert.True(controller.ViewData.ModelState.Count == 1);
        }

        [Fact]
        public void FetchPermissions_redirects_to_error_when_roleid_is_incorrect()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.FetchPermissions(null, "roleName");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);

            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned Controller is "Home"
            Assert.True(controller.ViewData.ModelState.Count == 1);
        }

        private PermissionsController ConfigureController()
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

            var controller = new PermissionsController(_bosIAClient)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = principal }
                }
            };

            return controller;
        }

        [Fact]
        public void BackToRoles_returns_roles_view()
        {
            //Arrange
            var controller = new PermissionsController(_bosIAClient);

            //Act
            var result = controller.BackToRoles();

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdatePermissions_returns_string_when_data_is_null()
        {
            //Arrange
            var controller = new PermissionsController(_bosIAClient);

            //Act
            var result = await controller.UpdatePermissions(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdatePermissions_returns_string_when_modules_is_null()
        {
            //Arrange
            var controller = new PermissionsController(_bosIAClient);

            //Act
            var result = await controller.UpdatePermissions(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdatePermissions_returns_string_when_operations_is_null()
        {
            //Arrange
            var controller = new PermissionsController(_bosIAClient);

            //Act
            var result = await controller.UpdatePermissions(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdatePermissions_returns_string_when_ownerId_is_null()
        {
            //Arrange
            var controller = new PermissionsController(_bosIAClient);

            //Act
            var result = await controller.UpdatePermissions(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdatePermissions_returns_string_when_data_passed_is_incorrect()
        {
            //Arrange
            var controller = new PermissionsController(_bosIAClient);

            //Act
            var result = await controller.UpdatePermissions(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }
    }
}
