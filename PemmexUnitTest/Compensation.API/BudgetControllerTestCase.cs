using Compensation.API.Commands.CreateBudgetCommand;
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
    public class BudgetControllerTestCase
    {
        private readonly ILogService _logService;
        BudgetController budgetController;
        public BudgetControllerTestCase()
        {
            budgetController = new BudgetController(_logService);
        }

        #region Post Test Case
        [TestMethod]
        public void Post_Test1()
        {
            CreateBudgetCommand createBudgetCommand = new CreateBudgetCommand();
            List<BudgetFunctions> budget = new List<BudgetFunctions>()
            {
                new BudgetFunctions(){
                budgetPercentage = 4,
                budgetValue = 34.90,
                jobFunction = PemmexCommonLibs.Domain.Enums.JobFunction.Sales 
                }
            };
            List<BudgetDetail> budgetDetail = new List<BudgetDetail>()
            {
                new BudgetDetail(){businessIdentifier = "123", budgetFunctions = budget}
            };
            createBudgetCommand.organizationIdentifier = "111";
            createBudgetCommand.startDate = DateTime.Now;
            createBudgetCommand.endDate = DateTime.Now;
            foreach (var item in budgetDetail)
            {
                createBudgetCommand.budgetDetails.Add(item);
            }
             
            Task<ActionResult<ResponseMessage>> result = budgetController.Post(createBudgetCommand) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        //#region Get Test Case
        //[TestMethod]
        //public void Get_Test1()
        //{
        //    string organizationIdentifier = "1234";

        //    Task<ActionResult<List<OrganizationBudgetDto>>> result = budgetController.Post(organizationIdentifier) as Task<ActionResult<List<OrganizationBudgetDto>>>;

        //    Assert.IsNotNull(result);
        //}
        //#endregion

        //#region GetFunctionalBudgetByManagerId Test Case
        //[TestMethod]
        //public void GetFunctionalBudgetByManagerId_Test1()
        //{
        //    string[] employees = new string[3];

        //    Task<ActionResult<List<FunctionalBudgetDto>>> result = budgetController.GetFunctionalBudgetByManagerId(employees) as Task<ActionResult<List<FunctionalBudgetDto>>>;

        //    Assert.IsNotNull(result);
        //}
        //#endregion


        #region SaveFunctionalBudget Test Case
        [TestMethod]
        public void SaveFunctionalBudget_Test1()
        {
            string organizationIdentifier = "1234";
            DateTime date = DateTime.Now;

            Task<ActionResult<ResponseMessage>> result = budgetController.SaveFunctionalBudget(organizationIdentifier, date) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
