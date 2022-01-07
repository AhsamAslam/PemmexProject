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

namespace Compensation.API.Queries.GetBusinessSalaries
{
    public class GetBusinessSalariesQuery : IRequest<List<CompensationDto>>
    {
        public string businessIdentifier { get; set; }
    }

    public class GetBusinessSalariesQueryHandeler : IRequestHandler<GetBusinessSalariesQuery, List<CompensationDto>>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public GetBusinessSalariesQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CompensationDto>> Handle(GetBusinessSalariesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salary = await _context.GetCurrentJobFunctionCompensations(request.businessIdentifier);
                return _mapper.Map<List<Database.Entities.Compensation>, List<CompensationDto>>(salary.ToList());
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
