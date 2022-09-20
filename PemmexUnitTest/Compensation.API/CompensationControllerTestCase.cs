using Compensation.API.Controllers;
using Compensation.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexUnitTest.Compensation.API
{
    [TestClass]
    public class CompensationControllerTestCase
    {
        private readonly ILogService _logService;
        CompensationController compensationController;
        public CompensationControllerTestCase()
        {
            compensationController = new CompensationController(_logService);
        }

        #region Get Test Case
        [TestMethod]
        public void Get_Test1()
        {
            string[] Identifiers = new string[3];

            Task<ActionResult<List<CompensationDto>>> result = compensationController.Get(Identifiers) as Task<ActionResult<List<CompensationDto>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region OrganizationCompensations Test Case
        [TestMethod]
        public void OrganizationCompensations_Test1()
        {
            string organizationIdentifier = "1234";

            Task<ActionResult<List<CompensationDto>>> result = compensationController.OrganizationCompensations(organizationIdentifier) as Task<ActionResult<List<CompensationDto>>>;

            Assert.IsNotNull(result);
        }
        #endregion

    }
}
