using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Organization.API.Controllers;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremexUnitTest.Organization.API
{
    [TestClass]
    public class OrganizationControllerTestCase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTime _dateTime;
        private readonly ILogService _logService;
        OrganizationController organizationController;
        public OrganizationControllerTestCase()
        {
            organizationController = new OrganizationController(_hostingEnvironment,
                _fileUploadService, _dateTime, _logService);
        }

        #region PostAsync Test Case
        [TestMethod]
        public void PostAsync_Test1()
        {
            
            Task<ActionResult<ResponseMessage>> result = organizationController.PostAsync("2233") as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region fullOrganization Test Case
        [TestMethod]
        public void fullOrganization_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = organizationController.fullOrganization("2233") as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region GetAsync Test Case
        [TestMethod]
        public void GetAsync_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = organizationController.GetAsync("2233") as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region GetManagersAsync Test Case
        [TestMethod]
        public void GetManagersAsync_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = organizationController.GetManagersAsync("2233") as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Businesses Test Case
        [TestMethod]
        public void Businesses_Test1()
        {

            Task<ActionResult<List<BusinessVM>>> result = organizationController.Businesses("2233") as Task<ActionResult<List<BusinessVM>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region GetAllOrganizationAsync Test Case
        [TestMethod]
        public void GetAllOrganizationAsync_Test1()
        {

            Task<ActionResult<List<EmployeeResponse>>> result = organizationController.GetAllOrganizationAsync("2233") as Task<ActionResult<List<EmployeeResponse>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region GetAllBusinessAsync Test Case
        [TestMethod]
        public void GetAllBusinessAsync_Test1()
        {

            Task<ActionResult<List<EmployeeResponse>>> result = organizationController.GetAllBusinessAsync("2233") as Task<ActionResult<List<EmployeeResponse>>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
