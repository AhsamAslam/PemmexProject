using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetCompensation
{
    public class GetCompensationQuery : IRequest<List<CompensationDto>>
    {
        public string[] Identifiers { get; set; }
    }
    public class GetCompensationQueryHandeler : IRequestHandler<GetCompensationQuery, List<CompensationDto>>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public GetCompensationQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CompensationDto>> Handle(GetCompensationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _context.GetCurrentCompensation(request.Identifiers);
                return _mapper.Map<List<Database.Entities.Compensation>, List<CompensationDto>>(employees.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
