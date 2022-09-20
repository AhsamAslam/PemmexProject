using Compensation.API.Commands.CreateBudgetCommand;
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

        #region Get Test Case
        [TestMethod]
        public void Get_Test1()
        {
            string organizationIdentifier = "1234";
            DateTime budgetDate = DateTime.Now;

            Task<ActionResult<ResponseMessage>> result = budgetController.Get(organizationIdentifier, budgetDate) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
