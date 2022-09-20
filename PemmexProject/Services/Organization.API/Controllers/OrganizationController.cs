using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Organization.API.Dtos;
using Organization.API.Queries.GetAllEmployeeTree;
using Organization.API.Queries.GetManagersQuery;
using Organization.API.Queries.GetOrganization;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using OfficeOpenXml;
using Organization.API.Commands.UploadOrganization;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using PemmexCommonLibs.Application.Interfaces;
using Organization.API.Queries.GetAllOrganizationEmployees;
using Organization.API.Queries.GetOrganizationEmployees;
using Organization.API.Queries.GetAllBusinessEmployees;
using Organization.API.Queries.GetBusinessUnits;
using Microsoft.AspNetCore.Authorization;
using PemmexCommonLibs.Application.Extensions;
using Organization.API.Database.Entities;
using Organization.API.Queries.GetBusinessUnitHeads;

namespace Organization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ApiControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTime _dateTime;
        private readonly ILogService _logService;

        public OrganizationController(IWebHostEnvironment hostEnvironment, 
            IFileUploadService fileUploadService,IDateTime dateTime,
            ILogService logService)
        {
            _hostingEnvironment = hostEnvironment;
            _fileUploadService = fileUploadService;
            _dateTime = dateTime;
            _logService = logService;

        }
        // POST /Upload Organization
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseMessage>> PostAsync(string organizationIdentifier)
        {
            try
            {
                //await _logService.WriteLogAsync(new Exception("hello Ahsam"), $"Organization_add{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                IFormFile file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string FileName = Guid.NewGuid().ToString() + "organization" + organizationIdentifier + _dateTime.Now.FormatDateTime();
                    await _fileUploadService.FileUploadToAzureAsync(file, FileName);
                    List<CostCenterUploadRequest> costCenterUploads = new List<CostCenterUploadRequest>();
                    List<BusinessRequestDto> businesses = new List<BusinessRequestDto>();
                    List<EmployeeUploadRequest> employeeUploadRequests = new List<EmployeeUploadRequest>();
                    var compensations = new List<CompensationUploadRequest>();
                    var stream = await _fileUploadService.FileDownloadFromAzureAsync(FileName);
                    //create a new Excel package in a memorystream
                    var memoryStream = new MemoryStream();
                    stream.CopyTo(memoryStream);
                    using (var package = new ExcelPackage(memoryStream))
                    {
                        var count = 0;
                        foreach(var sheet in package.Workbook.Worksheets)
                        {
                            
                            if (count ==2 )
                            {
                                for (int r = 0; r < sheet.Dimension.End.Row + 1; r++)
                                {
                                    if (r > 3 && !string.IsNullOrEmpty(sheet.Cells[r, 6].Value.ToString2()))
                                    {
                                        EmployeeUploadRequest emp_req = new EmployeeUploadRequest();
                                        emp_req.FirstName = sheet.Cells[r,1].Value.ToString2();
                                        emp_req.LastName = sheet.Cells[r, 2].Value.ToString2();
                                        emp_req.MiddleName = sheet.Cells[r, 3].Value.ToString2();
                                        emp_req.EmployeeDob = sheet.Cells[r, 4].Value.ToString2();
                                        emp_req.Title = sheet.Cells[r, 5].Value.ToString2();
                                        emp_req.EmployeeIdentifier = sheet.Cells[r, 6].Value.ToString2();
                                        emp_req.OrganizationIdentifier = sheet.Cells[r, 7].Value.ToString2();
                                        emp_req.Gender = sheet.Cells[r, 8].Value.ToString2();
                                        emp_req.ManagerIdentifier = sheet.Cells[r, 9].Value.ToString2();
                                        emp_req.CostCenterIdentifier = sheet.Cells[r, 10].Value.ToString2();
                                        emp_req.Grade = sheet.Cells[r, 11].Value.ToString2();
                                        emp_req.Shift = sheet.Cells[r, 12].Value.ToInt2();
                                        emp_req.StreetAddress = sheet.Cells[r, 13].Value.ToString2();
                                        emp_req.HouseNumber = sheet.Cells[r, 14].Value.ToString2();
                                        emp_req.Muncipality = sheet.Cells[r, 15].Value.ToString2();
                                        emp_req.PostalCode = sheet.Cells[r, 16].Value.ToString2();
                                        emp_req.Province = sheet.Cells[r, 17].Value.ToString2();
                                        emp_req.Country = sheet.Cells[r, 18].Value.ToString2();
                                        emp_req.CountryCellNumber = sheet.Cells[r, 19].Value.ToString2();
                                        emp_req.PhoneNumber = sheet.Cells[r, 20].Value.ToString2();
                                        emp_req.Email = sheet.Cells[r, 21].Value.ToString2();
                                        emp_req.Nationality = sheet.Cells[r, 22].Value.ToString2();
                                        emp_req.FirstLanguage = sheet.Cells[r, 23].Value.ToString2();
                                        emp_req.FirstLanguageSkills = sheet.Cells[r, 24].Value.ToString2() == "" ? null : sheet.Cells[r, 24].Value.ToString2().ToEnum<LanguageSkills>();
                                        emp_req.SecondLanguage = sheet.Cells[r, 25].Value.ToString2();
                                        emp_req.SecondLanguageSkills = sheet.Cells[r, 26].Value.ToString2() == "" ? null : sheet.Cells[r, 26].Value.ToString2().ToEnum<LanguageSkills>();
                                        emp_req.ThirdLanguage = sheet.Cells[r, 27].Value.ToString2();
                                        emp_req.ThirdLanguageSkills = sheet.Cells[r, 28].Value.ToString2() == "" ? null : sheet.Cells[r, 28].Value.ToString2().ToEnum<LanguageSkills>();
                                        emp_req.UserName = sheet.Cells[r, 35].Value.ToString2();
                                        emp_req.Role = sheet.Cells[r, 59].Value.ToString2();
                                        emp_req.JobFunction = sheet.Cells[r, 60].Value.ToString2() == "" ? null : sheet.Cells[r, 60].Value.ToString2().ToEnum<JobFunction>();
                                        emp_req.IsActive = true;

                                        List<EmployeeContactUpload> emp_contact = new List<EmployeeContactUpload>();
                                        EmployeeContactUpload contact1 = new EmployeeContactUpload();
                                        contact1.Name = sheet.Cells[r, 29].Value.ToString2();
                                        contact1.PhoneNumber = sheet.Cells[r, 30].Value.ToString2();
                                        contact1.Relationship = sheet.Cells[r, 31].Value.ToString2();
                                        
                                        if(!string.IsNullOrEmpty(contact1.PhoneNumber))
                                        emp_contact.Add(contact1);
                                        
                                        EmployeeContactUpload contact2 = new EmployeeContactUpload();
                                        contact2.Name = sheet.Cells[r, 32].Value.ToString2();
                                        contact2.PhoneNumber = sheet.Cells[r, 33].Value.ToString2();
                                        contact2.Relationship = sheet.Cells[r, 34].Value.ToString2();

                                        if (!string.IsNullOrEmpty(contact1.PhoneNumber))
                                            emp_contact.Add(contact2);

                                        emp_req.employeeContacts = emp_contact;

                                        CompensationUploadRequest compensation = new CompensationUploadRequest();
                                        compensation.EmployeeIdentifier = sheet.Cells[r, 6].Value.ToString2();
                                        compensation.BaseSalary = sheet.Cells[r, 36].Value.ToDouble2();
                                        compensation.AdditionalAgreedPart = sheet.Cells[r, 37].Value.ToDouble2();
                                        compensation.CarBenefit = sheet.Cells[r, 38].Value.ToDouble2();
                                        compensation.InsuranceBenefit = sheet.Cells[r, 39].Value.ToDouble2();
                                        compensation.PhoneBenefit = sheet.Cells[r, 40].Value.ToDouble2();
                                        compensation.EmissionBenefit = sheet.Cells[r, 41].Value.ToDouble2();
                                        compensation.HomeInternetBenefit = sheet.Cells[r, 42].Value.ToDouble2();
                                        compensation.TotalMonthlyPay = sheet.Cells[r, 43].Value.ToDouble2();
                                        compensation.EffectiveDate = DateTime.Now;

                                        compensations.Add(compensation);

                                        HolidayUploadRequest holidayUploadRequest = new HolidayUploadRequest();
                                        holidayUploadRequest.EmployeeIdentifier = sheet.Cells[r, 6].Value.ToString2();
                                        holidayUploadRequest.AnnualHolidaysEntitled = sheet.Cells[r, 44].Value.ToInt2();
                                        holidayUploadRequest.AccruedHolidaysPreviousYear = sheet.Cells[r, 45].Value.ToInt2();
                                        holidayUploadRequest.UsedHolidaysCurrentYear = sheet.Cells[r, 46].Value.ToInt2();
                                        holidayUploadRequest.LeftHolidaysCurrentYear = sheet.Cells[r, 47].Value.ToInt2();
                                        holidayUploadRequest.ParentalHolidays = sheet.Cells[r, 48].Value.ToInt2();
                                        holidayUploadRequest.AgreementAnnualHolidays = sheet.Cells[r, 49].Value.ToInt2();
                                        holidayUploadRequest.SickLeave = sheet.Cells[r, 50].Value.ToInt2();
                                        holidayUploadRequest.TimeOffWithoutSalary = sheet.Cells[r, 51].Value.ToInt2();
                                        holidayUploadRequest.EmployementStartDate = sheet.Cells[r, 52].Value.ToDateTime2();
                                        emp_req.holidayUploadRequest = holidayUploadRequest;
                                       


                                        //TimeTableUploadRequest timeTableUploadRequest = new TimeTableUploadRequest();
                                        //timeTableUploadRequest.EmployeeIdentifier = sheet.Cells[r, 6].Value.ToString2();
                                        //timeTableUploadRequest.FlexibleHours = sheet.Cells[r, 64].Value.ToDouble2();
                                        //timeTableUploadRequest.StartHours = sheet.Cells[r, 65].Value.ToDateTime2();
                                        //timeTableUploadRequest.EndHours = sheet.Cells[r, 66].Value.ToDateTime2();
                                        //timeTableUploadRequest.WeeklyHours = sheet.Cells[r, 67].Value.ToDouble2();
                                        //timeTableUploadRequest.TaskDescription = sheet.Cells[r, 68].Value.ToString2();
                                        //timeTableUploadRequest.FlexibleHrsAdvertiseAndAccept = sheet.Cells[r, 62].Value.ToDouble2();
                                        //emp_req.holidayUploadRequest = holidayUploadRequest;
                                        //employees.Add(emp_req);


                                        //oDetail.LegalOrgZero = sheet.Cells[r, 17].Value.ToString2();
                                        //oDetail.LegalOrgOne = sheet.Cells[r, 18].Value.ToString2();
                                        //oDetail.FunctionalOrgZero = sheet.Cells[r, 19].Value.ToString2();
                                        //oDetail.FunctionalOrgOne = sheet.Cells[r, 20].Value.ToString2();
                                        //oDetail.FunctionalOrgTwo = sheet.Cells[r, 21].Value.ToString2();
                                        //oDetail.FunctionalOrgThree = sheet.Cells[r, 22].Value.ToString2();
                                        //oDetail.FunctionalOrgFour = sheet.Cells[r, 23].Value.ToString2();
                                        //oDetail.FunctionalOrgFive = sheet.Cells[r, 24].Value.ToString2();
                                        

                                        employeeUploadRequests.Add(emp_req);
                                    }
                                }
                            }
                            else if (sheet.Name == "Organizations")
                            {
                                for (int r = 2; r < sheet.Dimension.End.Row + 1; r++)
                                {
                                    BusinessRequestDto o = new BusinessRequestDto();
                                    o.Name = sheet.Cells[r, 1].Value.ToString2();
                                    o.OrgNumber = sheet.Cells[r, 2].Value.ToString2();
                                    o.ParentNumber = sheet.Cells[r, 3].Value.ToString2();
                                    o.OrganizationCountry = sheet.Cells[r, 4].Value.ToString2();
                                    o.CurrencyCode = sheet.Cells[r, 5].Value.ToString2();
                                    o.FileName = FileName;
                                    businesses.Add(o);
                                }
                            }
                            else if (sheet.Name == "CostCenterDefinition")
                            {
                                for (int r = 2; ((r < sheet.Dimension.End.Row + 1) && sheet.Cells[r, 1].Value.ToString2() != ""); r++)
                                {
                                    CostCenterUploadRequest o = new CostCenterUploadRequest();
                                    o.CostCenterName = sheet.Cells[r, 3].Value.ToString2();
                                    o.CostCenterIdentifier = sheet.Cells[r, 1].Value.ToString2();
                                    o.businessIdentifier = sheet.Cells[r, 2].Value.ToString2();
                                    o.ParentCostCenterIdentifier = sheet.Cells[r, 4].Value.ToString2();
                                    costCenterUploads.Add(o);
                                }
                            }

                            count++;
                        }

                    }
                    if(businesses.Count > 0)
                    {
                        
                        UploadOrganizationCommand command = new UploadOrganizationCommand();
                        command.employeeUploadRequests = employeeUploadRequests;
                        command.businesses = businesses;
                        command.costCenterUploads = costCenterUploads;
                        command.compensationUploadRequests = compensations;
                        var e = await Mediator.Send(command);
                    }
                    else
                    {
                        return new ResponseMessage(true, EResponse.NoData, AppConstants._noData, null);
                    }
                }
                

                return new ResponseMessage(true, EResponse.OK, AppConstants._organizationCreated, null);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_add{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.ToString(), null);
            }
        }
        // GET full organization
        [HttpGet]
        [Route("fullOrganization")]
        public async Task<ActionResult<ResponseMessage>> fullOrganization()
        {
            try
            {
                var data = await Mediator.Send(new GetAllEmployeeTree { Id = CurrentUser.OrganizationIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }     
        [Authorize("Manager")]
        [HttpGet]
        [Route("managers")]
        public async Task<ActionResult<List<EmployeeResponse>>> GetManagersAsync()
        {
            try
            {
                 return await Mediator.Send(new GetManagersQuery { Id = CurrentUser.OrganizationIdentifier});
                //return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("Businesses")]
        public async Task<ActionResult<List<BusinessVM>>> Businesses()
        {
            try
            {
                return await Mediator.Send(new GetAllBusinessesQuery { Id = CurrentUser.OrganizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("OrganizationEmployees")]
        public async Task<ActionResult<List<EmployeeResponse>>> GetAllOrganizationAsync(string organizationIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetOrganizationEmployeesQuery { Id = organizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("BusinessEmployees")]
        public async Task<ActionResult<List<EmployeeResponse>>> GetAllBusinessAsync(string businessIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetAllBusinessEmployeesQuery { Id = businessIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("BusinessUnits")]
        public async Task<ActionResult<List<sp_GetBusinessUnitsDto>>> GetAllBusinessUnitsAsync(string organizationIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetBusinessUnitsQuery { organizationIdentifier = organizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("BusinessUnitHeads")]
        public async Task<ActionResult<List<EmployeeResponse>>> GetBusinessUnitHeadsAsync()
        {
            try
            {
                return await Mediator.Send(new GetBusinessUnitHeadsQuery { organizationIdentifier = CurrentUser.OrganizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Organization_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }



        //[AllowAnonymous]
        //[HttpPut]
        //[Route("UpdateCompensation")]
        //public async Task<ActionResult<ResponseMessage>> UpdateCompensation(UpdateCompensationRequest updateCompensationRequest)
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(updateCompensationRequest);
        //        return new ResponseMessage(true, EResponse.OK, AppConstants._employeeUpdated, data);

        //    }
        //    catch (Exception e)
        //    {
        //        return new ResponseMessage(false, EResponse.UnexpectedError, e.InnerException.Message, null);
        //    }
        //}
    }
}
