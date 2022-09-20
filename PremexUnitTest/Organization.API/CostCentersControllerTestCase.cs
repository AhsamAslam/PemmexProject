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

namespace PremexUnitTest.Organization.API
{
    [TestClass]
    public class CostCentersControllerTestCase
    {
        private readonly ILogService _logService;
        CostCentersController costCentersController;
        public CostCentersControllerTestCase()
        {
            costCentersController = new CostCentersController(_logService);
        }

        #region GetAsync Test Case
        [TestMethod]
        public void GetAsync_Test1()
        {

            Task<ActionResult<CostCenterResponse>> result = costCentersController.GetAsync("2233") as Task<ActionResult<CostCenterResponse>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Get Test Case
        [TestMethod]
        public void Get_Test1()
        {

            Task<ActionResult<List<CostCenterResponse>>> result = costCentersController.Get("2233") as Task<ActionResult<List<CostCenterResponse>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Tree Test Case
        [TestMethod]
        public void Tree_Test1()
        {

            Task<ActionResult<List<CostCenterResponse>>> result = costCentersController.Tree("2233") as Task<ActionResult<List<CostCenterResponse>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region PostAsync Test Case
        [TestMethod]
        public void PostAsync_Test1()
        {
            CostCenterRequest costCenterRequest = new CostCenterRequest();
            costCenterRequest.CostCenterIdentifier = "2233";
            costCenterRequest.ParentCostCenterIdentifier = "223";
            costCenterRequest.CostCenterName = "Pemmex";
            Task<ActionResult<int>> result = costCentersController.PostAsync(costCenterRequest) as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
