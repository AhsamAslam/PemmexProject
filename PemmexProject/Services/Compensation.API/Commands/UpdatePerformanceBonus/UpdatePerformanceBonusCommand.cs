using AutoMapper;
using Compensation.API.Database.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.UpdatePerformanceBonus
{
    public class UpdatePerformanceBonusCommand : IRequest
    {
        public List<UpdateBonusFromPerformanceBonusRequest> bonusFromPerformanceBonusRequests { get; set; }
    }
    public class UpdateBonusFromPerformanceBonusRequest
    {
        public string employeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public double bonusAmount { get; set; }
    }

    public class UpdatePerformanceBonusCommandHandeler : IRequestHandler<UpdatePerformanceBonusCommand>
    {
        private readonly ICompensationSalaryRepository _compensationSalaryRepository;
        private readonly IMapper _mapper;
        public UpdatePerformanceBonusCommandHandeler(
            IMapper mapper, ICompensationSalaryRepository compensationSalaryRepository)
        {
            _mapper = mapper;
            _compensationSalaryRepository = compensationSalaryRepository;
        }
        public async Task<Unit> Handle(UpdatePerformanceBonusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.bonusFromPerformanceBonusRequests)
                {
                    var salary = await _compensationSalaryRepository.GetCurrentSalary(item.employeeIdentifier);
                    if (salary == null)
                    {
                        List<string> employees = new List<string>();
                        employees.Add(item.employeeIdentifier);
                        var compensation = (await _compensationSalaryRepository.GetCurrentCompensation(employees.ToArray())).ToList();
                        if (compensation.Count > 0)
                        {
                            var s = _mapper.Map<Database.Entities.CompensationSalaries>(compensation.FirstOrDefault());
                            s.annual_bonus = (item.bonusAmount == 0) ? salary.annual_bonus : item.bonusAmount;
                            await _compensationSalaryRepository.SaveSalary(s);
                        }
                    }
                    else
                    {
                        salary.annual_bonus = (item.bonusAmount == 0) ? salary.annual_bonus : item.bonusAmount;
                        await _compensationSalaryRepository.SaveSalary(salary);
                    }
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
