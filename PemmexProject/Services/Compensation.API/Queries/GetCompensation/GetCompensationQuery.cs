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
    public class GetCompensationQuery : IRequest<CompensationDto>
    {
        public string employeeIdentifier { get; set; }
    }

    public class GetCompensationQueryHandeler : IRequestHandler<GetCompensationQuery, CompensationDto>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public GetCompensationQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompensationDto> Handle(GetCompensationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _context.GetCurrentCompensation(request.employeeIdentifier);
                return _mapper.Map<Database.Entities.Compensation, CompensationDto>(employee);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
