using Authentication.API.Commands;
using Authentication.API.Commands.ChangePassword;
using Authentication.API.Commands.DeactivateUser;
using Authentication.API.Commands.ForceChangePassword;
using Authentication.API.Commands.SaveRole;
using Authentication.API.Controllers;
using Authentication.API.Dtos;
using Authentication.API.Queries.GetAllUsers;
using AutoMapper;
using IdentityServer4;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services;
using System.Threading.Tasks;

namespace PremexUnitTest
{
    [TestClass]
    public class AuthenticationAPIUnitTests
    {
        private IConfiguration _configuration;
        private IdentityServerTools _tools;
        private IMapper _mapper;
        private ILogService _logService;
        Login login;
        UserController userController;
        public AuthenticationAPIUnitTests()
        {
            login = new Login(_configuration, _tools, _mapper, _logService);
            userController = new UserController(_logService);
        }
        #region Login Post Test Case
        [TestMethod]
        public void PostAuthenticationRequest_Test1()
        {
            AuthenticationRequest request = new AuthenticationRequest();
            request.UserName = "arqamawan@gmail.com";
            request.Password = "1234";

            Task<ActionResult<ResponseMessage>> result = login.Post(request) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Login login with two step test case
        [TestMethod]
        public void loginwithtwostep_Test1()
        {
            TwoStepLoginCommand request = new TwoStepLoginCommand();
            request.userName = "arqamawan@gmail.com";
            request.password = "1234";
            request.code = "123";

            Task<ActionResult<ResponseMessage>> result = login.loginwithtwostep(request) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Login Post Async Test Case
        [TestMethod]
        public void PostAsync_Test1()
        {
            ForceChangePasswordCommand request = new ForceChangePasswordCommand();
            request.UserId = new System.Guid();
            request.password = "123";

            Task<ActionResult<ResponseMessage>> result = login.PostAsync(request) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion


        #region Login send Password Email Test Case
        [TestMethod]
        public void sendPasswordEmail_Test1()
        {
            var Email = "arqamawan@gmail.com";

            Task<ActionResult<ResponseMessage>> result = login.sendPasswordEmail(Email) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Login ResetPassword Test Case
        [TestMethod]
        public void ResetPassword_Test1()
        {
            ChangePasswordCommand request = new ChangePasswordCommand();
            request.code = "123";
            request.password = "1234";
            request.email = "arqamawan@gmail.com";

            Task<ActionResult<ResponseMessage>> result = login.ResetPassword(request) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Login active directory login Test Case
        [TestMethod]
        public void activedirectorylogin_Test1()
        {
            var Email = "arqamawan@gmail.com";

            Task<ActionResult<ResponseMessage>> result = login.activedirectorylogin(Email) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion


        #region User get roles async Test Case
        [TestMethod]
        public void GetRolesAsync_Test1()
        {
            var result = DefaultDataService.GetDefaultRolesAndScreen("12234");

            Assert.IsNotNull(result);
        }
        #endregion

        #region User GetAllUsersAsync Test Case
        [TestMethod]
        public void GetAllUsersAsync_Test1()
        {
            Task<ActionResult<ResponseMessage>> result = userController.GetAllUsersAsync() as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region User roles PostAsync Test Case
        [TestMethod]
        public void RolesPostAsync_Test1()
        {
            RoleDto role = new RoleDto();
            role.role = PemmexCommonLibs.Domain.Enums.Roles.user;
            role.UserId = new System.Guid();
            Task<ActionResult<ResponseMessage>> result = userController.PostAsync(role) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region User DeactivateAsync Test Case
        [TestMethod]
        public void DeactivateAsync_Test1()
        {
            System.Guid id = new System.Guid();
            Task<ActionResult<ResponseMessage>> result = userController.DeactivateAsync(id) as Task<ActionResult<ResponseMessage>>;


            Assert.IsNotNull(result);
        }
        #endregion


    }
}
