using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Entities;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetOrganizationJobCatalogues
{
    public class GetOrganizationJobCataloguesQuery : IRequest<List<JobCatalogueDto>>
    {
        public string organizationIdentifier { get; set; }
    }

    public class GetOrganizationJobCataloguesQueryHandeler : IRequestHandler<GetOrganizationJobCataloguesQuery, List<JobCatalogueDto>>
    {
        private readonly IJobCatalogueRepository _context;
        private readonly IMapper _mapper;

        public GetOrganizationJobCataloguesQueryHandeler(IJobCatalogueRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<JobCatalogueDto>> Handle(GetOrganizationJobCataloguesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var jobcatalogues = await _context.GetOrganizationJobCatalogue(request.organizationIdentifier);

                return _mapper.Map<List<JobCatalogue>, List<JobCatalogueDto>>(jobcatalogues.ToList());
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
