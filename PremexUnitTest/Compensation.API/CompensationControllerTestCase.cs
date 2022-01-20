using Compensation.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremexUnitTest.Compensation.API
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
            string EmployeeIdentifier = "1234";

            Task<ActionResult<ResponseMessage>> result = compensationController.Get(EmployeeIdentifier) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

    }
}
