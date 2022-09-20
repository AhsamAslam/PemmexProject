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

namespace Compensation.API.Queries.GetSalary
{
    public class GetSalaryQuery : IRequest<CompensationSalariesDto>
    {
        public string employeeIdentifier { get; set; }
    }

    public class GetSalaryQueryHandeler : IRequestHandler<GetSalaryQuery, CompensationSalariesDto>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public GetSalaryQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompensationSalariesDto> Handle(GetSalaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salary = await _context.GetCurrentSalary(request.employeeIdentifier);
                return _mapper.Map<Database.Entities.CompensationSalaries, CompensationSalariesDto>(salary);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
