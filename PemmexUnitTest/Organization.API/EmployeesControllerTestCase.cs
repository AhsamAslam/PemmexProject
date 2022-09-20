using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Organization.API.Controllers;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexUnitTest.Organization.API
{
    [TestClass]
    public class EmployeesControllerTestCase
    {
        private readonly ILogService _logService;
        EmployeesController employeesController;
        public EmployeesControllerTestCase()
        {
            employeesController = new EmployeesController(_logService);
        }

        //#region GetAsync Test Case
        //[TestMethod]
        //public void GetAsync_Test1()
        //{
        //    string[] Identifiers = { "2233", "1122" };

        //    Task<ActionResult<List<EmployeeResponse>>> result = employeesController.GetAsync(Identifiers) as Task<ActionResult<List<EmployeeResponse>>>;

        //    Assert.IsNotNull(result);
        //}
        //#endregion

        #region ManagerTree Test Case
        [TestMethod]
        public void ManagerTree_Test1()
        {

            //Task<ActionResult<List<EmployeeResponse>>> result = employeesController.ManagerTree("22") as Task<ActionResult<List<EmployeeResponse>>>;

            //Assert.IsNotNull(result);
        }
        #endregion

        #region TeamMembers Test Case
        [TestMethod]
        public void TeamMembers_Test1()
        {

            Task<ActionResult<List<EmployeeResponse>>> result = employeesController.TeamMembers("22") as Task<ActionResult<List<EmployeeResponse>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region GetAsyncById Test Case
        [TestMethod]
        public void GetAsyncById_Test1()
        {

            Task<ActionResult<EmployeeResponse>> result = employeesController.GetAsync("22") as Task<ActionResult<EmployeeResponse>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region PostAsync Test Case
        [TestMethod]
        public void PostAsync_Test1()
        {
            EmployeeRequest employeeRequest = new EmployeeRequest();
            //TODO Pass the Data
            Task<ActionResult<int>> result = employeesController.PostAsync(employeeRequest) as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region PutAsync Test Case
        [TestMethod]
        public void PutAsync_Test1()
        {
            EmployeeRequest employeeRequest = new EmployeeRequest();
            //TODO Pass the Data
            Task<ActionResult<int>> result = employeesController.PutAsync(employeeRequest,"22") as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region DeleteAsync Test Case
        [TestMethod]
        public void DeleteAsync_Test1()
        {
            EmployeeRequest employeeRequest = new EmployeeRequest();
            //TODO Pass the Data
            Task<ActionResult<int>> result = employeesController.DeleteAsync(new Guid()) as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
