using BOS.Auth.Client;
using BOS.Email.Client;
using BOS.IA.Client;
using BOS.StarterCode.Controllers;
using BOS.StarterCode.Helpers;
using BOS.StarterCode.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BOS.StarterCode.Tests
{
    public class AuthControllerTests
    {
        private readonly IAuthClient _bosAuthClient;
        private readonly IIAClient _bosIAClient;
        private readonly IEmailClient _bosEmailClient;
        private readonly IConfiguration _configuration;

        public AuthControllerTests()
        {
            string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1YTE1NDNjOC00YzhhLTQ1M2EtOThjNS1kYmE1MTk4NjFlYWYiLCJzdWIiOiJCT1NBcGlLZXkiLCJpYXQiOjE1NzA1NjYwMzcsImFjY291bnQiOiIzMGM3NjkxMC1hYzU5LTRjOWItYmUyZS1kNGQ3OGJmZTBjZDAiLCJwcm9qZWN0IjoiM2RhY2FhZDQtYzE1ZC00NmY3LTk5YjktM2I3NDQ2MjVmYTdiIiwidGVuYW50IjoiNGU1MGNmNDItMzE4MS00N2RmLTk0ZGQtNzE5NTVlNmVmOTY1In0.ebHQCuto1BL3U1_xh8tIJdKqcv9fGMj43icx1edQ0yc";

            HttpClient httpClientAuth = new HttpClient();
            httpClientAuth.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            httpClientAuth.BaseAddress = new Uri("https://apis.dev.bosframework.com/auth/odata");
            AuthClient authClient = new AuthClient(httpClientAuth);

            HttpClient httpClientIA = new HttpClient();
            httpClientIA.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            httpClientIA.BaseAddress = new Uri("https://apis.dev.bosframework.com/ia/odata");
            IAClient iaClient = new IAClient(httpClientIA);

            HttpClient httpClientEmail = new HttpClient();
            httpClientEmail.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            httpClientEmail.BaseAddress = new Uri("https://apis.dev.bosframework.com/email/odata");
            EmailClient emailClient = new EmailClient(httpClientEmail);

            _bosAuthClient = authClient;
            _bosIAClient = iaClient;
            _bosEmailClient = emailClient;
            _configuration = null;
        }

        [Fact]
        public void Index_returns_login_view_when_not_authenticated()
        {
            //Arrange
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned view is "Index"
        }

        [Fact]
        public void Index_redirects_to_home_view_when_authenticated()
        {
            //Arrange 
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

            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = principal }
                }
            };

            //Act
            var result = controller.Index();

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); //Asserting that the return is a View
            Assert.Equal("Home", redirectResult.ControllerName); //Asseting that the returned Controller is "Home"
            Assert.Equal("Index", redirectResult.ActionName); //Asseting that the Action Methond of the controller is "Index"
        }

        [Fact]
        public async Task AuthenticateUser_without_cookie_consent_returns_message()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            AuthModel authModel = new AuthModel();

            //Act
            var result = await controller.AuthenticateUser(authModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned Controller is "Home"
            Assert.True(controller.ViewData.ModelState.Count == 1);
        }

        [Fact]
        public async Task AuthenticateUser_null_authobj_returns_index_view_with_modelstate_error()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add("Cookie", new CookieHeaderValue(".AspNet.Consent", "true").ToString());

            AuthModel authModel = null;

            //Act
            var result = await controller.AuthenticateUser(authModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned Controller is "Home"
            Assert.True(controller.ViewData.ModelState.Count == 1); //Asserting that there is a ModelError object
        }

        [Fact]
        public async Task AuthenticateUser_redirects_to_error_view_when_username_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add("Cookie", new CookieHeaderValue(".AspNet.Consent", "true").ToString());

            AuthModel authModel = new AuthModel
            {
                Username = null,
                Password = "password"
            };

            //Act
            var result = await controller.AuthenticateUser(authModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"

            //await Assert.ThrowsAsync<ArgumentNullException>(() => controller.AuthenticateUser(authModel));
        }

        [Fact]
        public async Task AuthenticateUser_redirects_to_index_view_with_incorrect_credentials()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers.Add("Cookie", new CookieHeaderValue(".AspNet.Consent", "true").ToString());

            AuthModel authModel = new AuthModel
            {
                Username = "username",
                Password = "password"
            };

            //Act
            var result = await controller.AuthenticateUser(authModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned view is "Index"
            Assert.True(controller.ViewData.ModelState.Count == 1); //Asserting that there is a ModelError object
        }

        [Fact]
        public async Task RegisterUser_redirects_to_error_view_when_registerobj_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            RegistrationModel registerObj = null;

            //Act
            var result = await controller.RegisterUser(registerObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task RegisterUser_redirects_to_error_view_when_email_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            RegistrationModel registerObj = new RegistrationModel
            {
                EmailAddress = null,
                FirstName = "John",
                LastName = "Doe"
            };

            //Act
            var result = await controller.RegisterUser(registerObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task RegisterUser_redirects_to_error_view_when_firstname_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            RegistrationModel registerObj = new RegistrationModel
            {
                EmailAddress = "john@doe.com",
                FirstName = null,
                LastName = "Doe"
            };

            //Act
            var result = await controller.RegisterUser(registerObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task RegisterUser_redirects_to_error_view_when_lastname_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            RegistrationModel registerObj = new RegistrationModel
            {
                EmailAddress = "john@doe.com",
                FirstName = "John",
                LastName = null
            };

            //Act
            var result = await controller.RegisterUser(registerObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task RegisterUser_redirects_to_login_on_successful_registration()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            RegistrationModel registerObj = new RegistrationModel
            {
                EmailAddress = "john@doe.com",
                FirstName = "John",
                LastName = "Doe"
            };

            //Act
            var result = await controller.RegisterUser(registerObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned view is "Index"
            Assert.True(controller.ViewData.ModelState.Count == 1);
        }

        [Fact]
        public async Task ForgotPasswordAction_redirects_to_error_view_when_object_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            ForgotPassword forgotPasswordObj = null;

            //Act
            var result = await controller.ForgotPasswordAction(forgotPasswordObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task ForgotPasswordAction_redirects_to_error_view_when_email_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            ForgotPassword forgotPasswordObj = new ForgotPassword
            {
                EmailAddress = null
            };

            //Act
            var result = await controller.ForgotPasswordAction(forgotPasswordObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task ForgotPasswordAction_incorrectemailadressformat()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            ForgotPassword forgotPasswordObj = new ForgotPassword
            {
                EmailAddress = null
            };

            //Act
            var result = await controller.ForgotPasswordAction(forgotPasswordObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task ForgotPasswordAction_redirects_to_error_view_when_email_is_in_incorrect_format()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            ForgotPassword forgotPasswordObj = new ForgotPassword
            {
                EmailAddress = null
            };

            //Act
            var result = await controller.ForgotPasswordAction(forgotPasswordObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "Error Page"
        }

        [Fact]
        public async Task ForgotPasswordAction_returns_to_login_view_when_valid_email()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            ForgotPassword forgotPasswordObj = new ForgotPassword
            {
                EmailAddress = null
            };

            //Act
            var result = await controller.ForgotPasswordAction(forgotPasswordObj);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned view is "Login"
        }

        [Fact]
        public async Task ForcePasswordChange_redirects_to_error_view_when_data_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            JObject data = null;

            //Act
            var result = await controller.ForcePasswordChange(data);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned view is "ErrorPage"
        }

        [Fact]
        public async Task ForcePasswordChange_redirects_to_error_view_when_userId_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            JObject data = null;

            //Act
            var result = await controller.ForcePasswordChange(data);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "ErrorPage"
        }

        [Fact]
        public async Task ForcePasswordChange_redirects_to_error_view_when_password_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            JObject data = null;

            //Act
            var result = await controller.ForcePasswordChange(data);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "ErrorPage"
        }

        [Fact]
        public async Task ForcePasswordChange_redirects_to_error_view_when_userid_is_in_incorrect_format()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            JObject data = null;

            //Act
            var result = await controller.ForcePasswordChange(data);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "ErrorPage"
        }

        [Fact]
        public async Task ForcePasswordChange_redirects_to_index_view_when_userid_is_unknown()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);
            JObject data = null;

            //Act
            var result = await controller.ForcePasswordChange(data);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Ixdex", viewResult.ViewName); //Asseting that the returned view is "Login"
        }

        [Fact]
        public void HasSessionExpired_throws_expection_when_httpcontext_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.HasSessionExpired();

            //Assert
            var viewResult = Assert.IsType<bool>(result);
            Assert.True(viewResult);
        }

        [Fact]
        public void HasSessionExpired_throws_expection_when_session_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.HasSessionExpired();

            //Assert
            var viewResult = Assert.IsType<bool>(result);
            Assert.True(viewResult);
        }

        [Fact]
        public void HasSessionExpired_returns_true_when_session_found()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.HasSessionExpired();

            //Assert
            var viewResult = Assert.IsType<bool>(result);
            Assert.True(viewResult);
        }

        [Fact]
        public void HasSessionExpired_returns_false_when_session_not_found()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);

            //Act
            var result = controller.HasSessionExpired();

            //Assert
            var viewResult = Assert.IsType<bool>(result);
            Assert.True(viewResult);
        }

        [Fact]
        public async Task Logout_redirects_to_error_view_when_htttpcontext_is_null()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.Logout();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("ErrorPage", viewResult.ViewName); //Asseting that the returned view is "ErrorPage"
        }

        [Fact]
        public async Task Logout_redirects_to_login_view_on_success()
        {
            //Arrange 
            var controller = new AuthController(_bosAuthClient, _bosIAClient, _bosEmailClient, _configuration);

            //Act
            var result = await controller.Logout();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Asserting that the return is a View
            Assert.Equal("Index", viewResult.ViewName); //Asseting that the returned view is "Index"
        }
    }
}
