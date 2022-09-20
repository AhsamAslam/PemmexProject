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
    public class BonusControllerTestCase
    {
        private ILogService _logService;
        BonusController bonusController;
        public BonusControllerTestCase()
        {
            bonusController = new BonusController(_logService);
        }

        #region GetUser Test Case
        [TestMethod]
        public void GetUser_Test1()
        {
            var EmployeeIdentifier = "1234";

            Task<ActionResult<ResponseMessage>> result = bonusController.Get(EmployeeIdentifier) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region TeamBonuses Test Case
        [TestMethod]
        public void TeamBonuses_Test1()
        {
            List<string> EmployeeIdentifiers = new List<string>();
            EmployeeIdentifiers.Add("123");
            EmployeeIdentifiers.Add("1234");
            EmployeeIdentifiers.Add("1235");

            Task<ActionResult<ResponseMessage>> result = bonusController.TeamBonuses(EmployeeIdentifiers) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region OrgnaizationBonuses Test Case
        [TestMethod]
        public void OrgnaizationBonuses_Test1()
        {
            string orgnaizationIdentifier = "1234";

            Task<ActionResult<ResponseMessage>> result = bonusController.OrgnaizationBonuses(orgnaizationIdentifier) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
