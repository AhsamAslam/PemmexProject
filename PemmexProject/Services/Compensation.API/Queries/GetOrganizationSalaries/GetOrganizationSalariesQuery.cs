using AutoMapper;
using Compensation.API.Database.Entities;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetOrganizationSalaries
{
    public class GetOrganizationSalariesQuery : IRequest<List<CompensationSalariesDto>>
    {
        public string organizationIdentifier { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class GetOrganizationSalariesQueryHandeler : IRequestHandler<GetOrganizationSalariesQuery, List<CompensationSalariesDto>>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public GetOrganizationSalariesQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CompensationSalariesDto>> Handle(GetOrganizationSalariesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salary = await _context.GetOrganizationCurrentSalary(request.organizationIdentifier,request.startDate,request.endDate);
                return _mapper.Map<List<CompensationSalaries>, List<CompensationSalariesDto>>(salary.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
