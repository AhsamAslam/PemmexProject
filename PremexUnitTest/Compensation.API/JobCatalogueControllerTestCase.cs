using Compensation.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremexUnitTest.Compensation.API
{
    [TestClass]
    public class JobCatalogueControllerTestCase
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTime _dateTime;
        private readonly ILogService _logService;
        JobCatalogueController jobCatalogueController;
        public JobCatalogueControllerTestCase()
        {
            jobCatalogueController = new JobCatalogueController(_fileUploadService, _dateTime, _logService);
        }

        #region Post Test Case
        [TestMethod]
        public void Post_Test1()
        {
            string OrganizationIdentifier = "1234";

            Task<ActionResult<ResponseMessage>> result = jobCatalogueController.Post(OrganizationIdentifier) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Get Test Case
        [TestMethod]
        public void Get_Test1()
        {
            string businessIdentifier = "111";
            JobFunction jobFunction = JobFunction.Sales;
            string grade = "B";

            Task<ActionResult<ResponseMessage>> result = jobCatalogueController.Get(businessIdentifier, jobFunction, grade) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

    }
}
