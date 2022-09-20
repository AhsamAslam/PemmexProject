using Holidays.API.Commands.SaveHolidays;
using Holidays.API.Controllers;
using Holidays.API.Dtos;
using Holidays.API.Repositories.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremexUnitTest.Holidays.API
{
    [TestClass]
    public class HolidaysControllerTestCase
    {
        private IEmployeeHolidays _employeeHolidays;
        private readonly ILogService _logService;
        Holiday holiday;
        public HolidaysControllerTestCase()
        {
            holiday = new Holiday(_employeeHolidays, _logService);
        }

        #region HolidayTypes Test Case
        [TestMethod]
        public void HolidayTypes_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = holiday.HolidayTypes() as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region SaveHolidaySettings Test Case
        [TestMethod]
        public void SaveHolidaySettings_Test1()
        {
            List<HolidaySettingsDto> holidaySettingsDtos = new List<HolidaySettingsDto>();
            for (int i = 0; i < 3; i++)
            {
                HolidaySettingsDto holidaySettingsDto = new HolidaySettingsDto();
                holidaySettingsDto.BusinessIdentifier = "123";
                holidaySettingsDto.HolidaySettingsId = 1;
                holidaySettingsDto.HolidaySettingsIdentitfier = new Guid();
                holidaySettingsDto.OrganizationIdentifier = "2334";
                holidaySettingsDto.HolidayCalendarYear = DateTime.Now;
                holidaySettingsDto.MaximumLimitHolidayToNextYear = 45;
            }

            Task<ActionResult<ResponseMessage>> result = holiday.SaveHolidaySettings(holidaySettingsDtos) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region SaveHolidaySetting Test Case
        [TestMethod]
        public void SaveHolidaySetting_Test1()
        {
            HolidaySettingsDto holidaySettingsDto = new HolidaySettingsDto();
            holidaySettingsDto.BusinessIdentifier = "123";
            holidaySettingsDto.HolidaySettingsId = 1;
            holidaySettingsDto.HolidaySettingsIdentitfier = new Guid();
            holidaySettingsDto.OrganizationIdentifier = "2334";
            holidaySettingsDto.HolidayCalendarYear = DateTime.Now;
            holidaySettingsDto.MaximumLimitHolidayToNextYear = 45;

            Task<ActionResult<ResponseMessage>> result = holiday.SaveHolidaySetting(holidaySettingsDto) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region GetHolidaySettings Test Case
        [TestMethod]
        public void GetHolidaySettings_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = holiday.GetHolidaySettings() as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region SaveHolidays Test Case
        [TestMethod]
        public void SaveHolidays_Test1()
        {
            SaveHolidayCommand saveHolidayCommand = new SaveHolidayCommand();
            saveHolidayCommand.EmployeeId = new Guid();
            saveHolidayCommand.EmployeeIdentifier = "223";
            saveHolidayCommand.costcenterIdentifier = "223";
            saveHolidayCommand.SubsituteId = new Guid();
            saveHolidayCommand.SubsituteIdentifier = "223";
            saveHolidayCommand.HolidayStartDate = DateTime.Now;
            saveHolidayCommand.HolidayEndDate = DateTime.Now;
            saveHolidayCommand.Message = "122";
        Task<ActionResult<ResponseMessage>> result = holiday.SaveHolidays(saveHolidayCommand) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region HolidayCounter Test Case
        [TestMethod]
        public void HolidayCounter_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = holiday.HolidayCounter() as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region UserEarnedHolidays Test Case
        [TestMethod]
        public void UserEarnedHolidays_Test1()
        {

            Task<ActionResult<int>> result = holiday.UserEarnedHolidays("2233", new Guid()) as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region UserPlannedHolidays Test Case
        [TestMethod]
        public void UserPlannedHolidays_Test1()
        {

            Task<ActionResult<int>> result = holiday.UserPlannedHolidays("2233", new Guid()) as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region UserRemainingHolidays Test Case
        [TestMethod]
        public void UserRemainingHolidays_Test1()
        {

            Task<ActionResult<int>> result = holiday.UserRemainingHolidays("2233", new Guid(), "USA") as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region UserAvailedHolidays Test Case
        [TestMethod]
        public void UserAvailedHolidays_Test1()
        {

            Task<ActionResult<int>> result = holiday.UserAvailedHolidays("2233", new Guid(), "USA") as Task<ActionResult<int>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region TeamHolidays Test Case
        [TestMethod]
        public void TeamHolidays_Test1()
        {

            Task<ActionResult<List<TakenHolidayDto>>> result = holiday.TeamHolidays("2233", "233") as Task<ActionResult<List<TakenHolidayDto>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region CreateCalendar Test Case
        [TestMethod]
        public void CreateCalendar_Test1()
        {

            Task<ActionResult<Unit>> result = holiday.CreateCalendar("2233") as Task<ActionResult<Unit>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region BusinessEmployeesHolidays Test Case
        [TestMethod]
        public void BusinessEmployeesHolidays_Test1()
        {

            Task<ActionResult<List<EmployeeHolidaysCounter>>> result = holiday.BusinessEmployeesHolidays("2233") as Task<ActionResult<List<EmployeeHolidaysCounter>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Calendar Test Case
        [TestMethod]
        public void Calendar_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = holiday.Calendar("2233") as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

    }
}
