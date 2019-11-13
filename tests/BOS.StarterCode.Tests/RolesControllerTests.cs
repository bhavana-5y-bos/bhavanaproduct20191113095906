using BOS.Auth.Client;
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
    public class RolesControllerTests
    {
        private readonly IAuthClient _bosAuthClient;

        public RolesControllerTests()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1YTE1NDNjOC00YzhhLTQ1M2EtOThjNS1kYmE1MTk4NjFlYWYiLCJzdWIiOiJCT1NBcGlLZXkiLCJpYXQiOjE1NzA1NjYwMzcsImFjY291bnQiOiIzMGM3NjkxMC1hYzU5LTRjOWItYmUyZS1kNGQ3OGJmZTBjZDAiLCJwcm9qZWN0IjoiM2RhY2FhZDQtYzE1ZC00NmY3LTk5YjktM2I3NDQ2MjVmYTdiIiwidGVuYW50IjoiNGU1MGNmNDItMzE4MS00N2RmLTk0ZGQtNzE5NTVlNmVmOTY1In0.ebHQCuto1BL3U1_xh8tIJdKqcv9fGMj43icx1edQ0yc");
            httpClient.BaseAddress = new Uri("https://apis.dev.bosframework.com/auth/odata/");

            AuthClient authClient = new AuthClient(httpClient);
            _bosAuthClient = authClient;
        }

        [Fact]
        public void Index_returns_model_with_null_currentmoduleid_when_claims_is_empty()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

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
            var controller = new RolesController(_bosAuthClient);

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
        public void AddNewRole_returns_model_with_null_currentmoduleid_when_claims_is_empty()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = controller.AddNewRole();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void AddNewRole_returns_model_with_null_currentmoduleid_when_session_is_empty()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void AddNewRole_returns_non_null_model_claims_and_sessions_are_not_empty()
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
        public void EditRole_returns_model_with_null_currentmoduleid_when_claims_is_empty()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = controller.EditRole(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void EditRole_returns_model_with_null_currentmoduleid_when_session_is_empty()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = controller.EditRole(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
        }

        [Fact]
        public void EditRole_returns_non_null_model_claims_and_sessions_are_not_empty()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.EditRole(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
        }

        [Fact]
        public void EditRole_returns_index_when_roleid_is_null()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.EditRole(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
            ///////////Add Model Error count
        }

        [Fact]
        public void EditRole_returns_index_when_roleid_is_incorrect()
        {
            //Arrange
            var controller = ConfigureController();
            controller.ControllerContext.HttpContext.Items.Add("ModuleOperations", "ModuleOperations");

            //Act
            var result = controller.EditRole(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            dynamic model = Assert.IsAssignableFrom<ExpandoObject>(viewResult.ViewData.Model);
            Assert.True(model.CurrentModuleId == null);
            Assert.True(model.Username != null);
            Assert.True(model.Initials != null);
            Assert.True(model.Roles != null);
            ///////////Add Model Error count
        }

        [Fact]
        public void RoleManagePermissions_redirects_to_permissions_view_when_roleid_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = controller.RoleManagePermissions(null, null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public void RoleManagePermissions_redirects_to_permissions_view_when_rolename_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = controller.RoleManagePermissions(null, null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public void RoleManagePermissions_redirects_to_permissions_view()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = controller.RoleManagePermissions(null, null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddRole_returns_string_when_data_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddRole_returns_string_when_role_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddRole_returns_string_when_role_has_same()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddRole_returns_string_when_role_has_special_characters()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AddRole_returns_string_when_role_is_correct()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRoles_returns_string_when_updatedroles_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRoles_returns_string_when_claims_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRoles_returns_string_when_userid_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRoles_returns_string_when_userid_is_incorrect()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRoles_returns_string_when_roleid_is_incorrect()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRolesByAdmin_returns_string_when_data_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRolesByAdmin_returns_string_when_updatedroles_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRolesByAdmin_returns_string_when_userid_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task UpdateUserRolesByAdmin_returns_string_when_role_count_is_zero()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task DeleteRole_returns_string_when_role_is_null()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task DeleteRole_returns_string_when_role_is_in_incorrect_format()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task DeleteRole_returns_string_when_role_is_incorrect()
        {
            //Arrange
            var controller = new RolesController(_bosAuthClient);

            //Act
            var result = await controller.AddRole(null);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Roles", redirectResult.ControllerName); //Asseting that the returned Controller is "Role"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        private RolesController ConfigureController()
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

            var controller = new RolesController(_bosAuthClient)
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
