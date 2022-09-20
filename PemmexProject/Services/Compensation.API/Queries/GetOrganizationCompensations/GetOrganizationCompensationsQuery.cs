using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetOrganizationCompensations
{
    public class GetOrganizationCompensationsQuery : IRequest<List<CompensationDto>>
    {
        public string organizationIdentifier { get; set; }
    }

    public class GetOrganizationCompensationsQueryHandeler : IRequestHandler<GetOrganizationCompensationsQuery, List<CompensationDto>>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public GetOrganizationCompensationsQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CompensationDto>> Handle(GetOrganizationCompensationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salary = await _context.GetOrganizationCurrentCompensations(request.organizationIdentifier);
                return _mapper.Map<List<Database.Entities.Compensation>, List<CompensationDto>>(salary.ToList());
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
