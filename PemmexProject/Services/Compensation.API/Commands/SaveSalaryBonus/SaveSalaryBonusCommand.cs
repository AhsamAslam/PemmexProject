using AutoMapper;
using Compensation.API.Database.context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.SaveSalaryBonus
{
    public class SaveSalaryBonusCommand : IRequest
    {
        public double one_time_bonus { get; set; }
        public DateTime IssuedDate { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
    }

    public class SaveBonusCommandHandeler : IRequestHandler<SaveSalaryBonusCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaveBonusCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(SaveSalaryBonusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var salary = await _context.CompensationSalaries
                   .FirstOrDefaultAsync(c => c.EmployeeIdentifier == request.EmployeeIdentifier
                   && (c.IssuedDate.Year == request.IssuedDate.Year
                   && c.IssuedDate.Month == request.IssuedDate.Month));

                if (salary == null)
                {
                    var compensation = _mapper.Map<Database.Entities.CompensationSalaries>(request);
                    _context.CompensationSalaries.Add(compensation);
                }
                else
                {
                    salary.one_time_bonus = (request.one_time_bonus == 0) ? salary.one_time_bonus : request.one_time_bonus;
                    _context.CompensationSalaries.Update(salary);
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
