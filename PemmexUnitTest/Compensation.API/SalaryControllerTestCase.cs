using Compensation.API.Controllers;
using Compensation.API.Dtos;
using Compensation.API.Queries.GetFunctionalBudgetCount;
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
    public class SalaryControllerTestCase
    {
        private readonly ILogService _logService;
        SalaryController salaryController;
        public SalaryControllerTestCase()
        {
            salaryController = new SalaryController(_logService);
        }

        #region GetAsync Test Case
        [TestMethod]
        public void GetAsync_Test1()
        {
            string EmployeeIdentifier = "1234";

            Task<ActionResult<ResponseMessage>> result = salaryController.GetAsync(EmployeeIdentifier) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Post Test Case
        [TestMethod]
        public void Post_Test1()
        {
            string EmployeeIdentifier = "1234";

            Task<ActionResult<ResponseMessage>> result = salaryController.Post(EmployeeIdentifier) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region OrganizationTotalSalaryCount Test Case
        [TestMethod]
        public void OrganizationTotalSalaryCount_Test1()
        {
            //string organizationIdentifier = "1234";

            //Task<ActionResult<ResponseMessage>> result = salaryController.OrganizationTotalSalaryCount(organizationIdentifier) as Task<ActionResult<ResponseMessage>>;

            //Assert.IsNotNull(result);
        }
        #endregion

        #region GetFunctionalSalaryCount Test Case
        [TestMethod]
        public void GetFunctionalSalaryCount_Test1()
        {
            List<string> EmployeeIdentifiers = new List<string>();
            EmployeeIdentifiers.Add("111");
            EmployeeIdentifiers.Add("222");
            EmployeeIdentifiers.Add("333");

            Task<ActionResult<double>> result = salaryController.GetFunctionalSalaryCount(EmployeeIdentifiers.ToArray()) as Task<ActionResult<double>>;

            Assert.IsNotNull(result);
        }
        #endregion


        #region GetFunctionalBudgetCount Test Case
        [TestMethod]
        public void GetFunctionalBudgetCount_Test1()
        {
            GetFunctionalBudgetCountQuery request = new GetFunctionalBudgetCountQuery();

            Task<ActionResult<List<OrganizationBudgetDto>>> result = salaryController.GetFunctionalBudgetCount(request) as Task<ActionResult<List<OrganizationBudgetDto>>>;

            Assert.IsNotNull(result);
        }
        #endregion

    }
}
