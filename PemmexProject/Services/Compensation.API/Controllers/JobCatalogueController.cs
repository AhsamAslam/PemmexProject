using Compensation.API.Commands.UploadJobCatalogue;
using Compensation.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Organization.API.Queries.GetJobCatalogue;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Compensation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ClientIdPolicy")]
    public class JobCatalogueController : ApiControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTime _dateTime;
        public JobCatalogueController(IFileUploadService fileUploadService, IDateTime dateTime)
        {
            _fileUploadService = fileUploadService;
            _dateTime = dateTime;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> Post(string OrganizationIdentifier)
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string FileName = Guid.NewGuid().ToString() + "jobcatalogue" + OrganizationIdentifier + _dateTime.Now.FormatDateTime();
                    await _fileUploadService.FileUploadToAzureAsync(file, FileName);
                    List<JobCatalogueDto> jobCatalogueDtos = new List<JobCatalogueDto>();

                    var stream = await _fileUploadService.FileDownloadFromAzureAsync(FileName);
                    //create a new Excel package in a memorystream
                    var memoryStream = new MemoryStream();
                    stream.CopyTo(memoryStream);
                    using (var package = new ExcelPackage(memoryStream))
                    {
                        foreach (var sheet in package.Workbook.Worksheets)
                        {

                            if (sheet.Name == "Salary")
                            {
                                for (int r = 2; r < sheet.Dimension.End.Row + 1; r++)
                                {
                                    JobCatalogueDto o = new JobCatalogueDto();
                                    o.grade = sheet.Cells[r, 1].Value.ToString2();
                                    o.minimum_salary = sheet.Cells[r, 2].Value.ToDouble2();
                                    o.median_salary = sheet.Cells[r, 3].Value.ToDouble2();
                                    o.maximum_salary = sheet.Cells[r, 4].Value.ToDouble2();
                                    o.annual_bonus = sheet.Cells[r, 5].Value.ToDouble2();
                                    o.country = sheet.Cells[r, 6].Value.ToString2();
                                    o.currency = sheet.Cells[r, 7].Value.ToString2();
                                    o.jobFunction = sheet.Cells[r, 8].Value.ToString2();
                                    o.acv_bonus_percentage = sheet.Cells[r, 9].Value.ToDouble2();
                                    o.organizationIdentifier = sheet.Cells[r, 10].Value.ToString2();

                                    jobCatalogueDtos.Add(o);
                                }
                            }
                        }

                    }
                    if (jobCatalogueDtos.Count > 0)
                    {

                        UploadJobCatalogueCommand command = new UploadJobCatalogueCommand();
                        command.JobCatalogueDtos = jobCatalogueDtos;
                        var e = await Mediator.Send(command);
                    }
                    else
                    {
                        return new ResponseMessage(true, EResponse.NoData, AppConstants._noData, null);
                    }
                }


                return new ResponseMessage(true, EResponse.OK, AppConstants._jobCatalogCreated, null);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.ToString(), null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> Get(string businessIdentifier,
            JobFunction jobFunction, string grade)
        {
            try
            {
                var data = await Mediator.Send(new GetJobCatalogueQuery
                {
                    organizationIdentifier = businessIdentifier
                ,
                    grade = grade,
                    jobFunction = jobFunction
                });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
    }
}
