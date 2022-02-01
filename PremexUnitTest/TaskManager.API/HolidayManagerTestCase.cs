using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.API.Commands.ApplyHoliday;
using TaskManager.API.Controllers;

namespace PremexUnitTest.TaskManager.API
{
    [TestClass]
    public class HolidayManagerTestCase
    {
        private readonly ILogService _logService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTime _dateTime;
        HolidayManager holidayManager;
        public HolidayManagerTestCase()
        {
            holidayManager = new HolidayManager(_logService, _fileUploadService, _dateTime);
        }

        #region ApplyHoliday Test Case
        [TestMethod]
        public void ApplyHoliday_Test1()
        {
            ApplyHolidayCommand applyHolidayCommand = new ApplyHolidayCommand();
            applyHolidayCommand.SubsituteId = new Guid();
            applyHolidayCommand.SubsituteIdentifier = "2233";
            applyHolidayCommand.HolidayStartDate = DateTime.Now;
            applyHolidayCommand.HolidayEndDate = DateTime.Now;
            applyHolidayCommand.EmployeeIdentifier = "223";
            applyHolidayCommand.ManagerIdentifier = "3344";
            applyHolidayCommand.taskDescription = "abc";
            applyHolidayCommand.UserId = new Guid();
            applyHolidayCommand.FileName = "abc";
            applyHolidayCommand.organizationIdentifier = "1122"; 
        Task<ActionResult<ResponseMessage>> result = holidayManager.ApplyHoliday(applyHolidayCommand) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

    }
}
