using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Entities;
using Compensation.API.Database.Repositories.Interface;
using Compensation.API.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetJobCatalogue
{
    public class GetJobCatalogueQuery : IRequest<JobCatalogueDto>
    {
        public string organizationIdentifier { get; set; }
        public JobFunction jobFunction { get; set; }
        public string grade { get; set; }
    }

    public class GetJobCatalogueQueryHandeler : IRequestHandler<GetJobCatalogueQuery, JobCatalogueDto>
    {
        private readonly IJobCatalogue _jobCatalogue;
        private readonly IMapper _mapper;

        public GetJobCatalogueQueryHandeler( IJobCatalogue jobCatalogue, IMapper mapper)
        {
            _jobCatalogue = jobCatalogue;
            _mapper = mapper;
        }
        public async Task<JobCatalogueDto> Handle(GetJobCatalogueQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var p = request.jobFunction.ToString();
                //var employee = await _context.JobCatalogues
                //    .Where(e => e.jobFunction == request.jobFunction.ToString() 
                //    && e.organizationIdentifier == request.organizationIdentifier
                //    && e.grade == request.grade
                //    )
                //    .FirstOrDefaultAsync(cancellationToken);
                var employee = await _jobCatalogue.GetJobCatalogueByOrganizationIdentifierAndJobFunctionAndGrade(request.jobFunction.ToString(), request.organizationIdentifier, request.grade);

                return _mapper.Map<JobCatalogue, JobCatalogueDto>(employee);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
