using AutoMapper;
using Compensation.API.Database.context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.UpdateCompensationAndBonus
{
    public class UpdateCompensationAndBonusCommand : IRequest
    {
        public double AdditionalAgreedPart { get; set; }
        public double BaseSalary { get; set; }
        public double TotalMonthlyPay { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public double one_time_bonus { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public TaskType TaskType { get; set; }
    }

    public class UpdateCompensationAndBonusCommandHandeler : IRequestHandler<UpdateCompensationAndBonusCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateCompensationAndBonusCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCompensationAndBonusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.TaskType == TaskType.Compensation)
                {
                    var compensation = _mapper.Map<Database.Entities.Compensation>(request);
                    _context.Compensation.Add(compensation);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else if(request.TaskType == TaskType.Bonus)
                {
                    var salary = await _context.CompensationSalaries
                   .FirstOrDefaultAsync(c => c.EmployeeIdentifier == request.EmployeeIdentifier
                   && (c.IssuedDate.Year == request.EffectiveDate.Year
                   && c.IssuedDate.Month == request.EffectiveDate.Month));

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
                }
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
