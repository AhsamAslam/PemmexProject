using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
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
        public double IncrementPercentage { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public TaskType TaskType { get; set; }
    }

    public class UpdateCompensationAndBonusCommandHandeler : IRequestHandler<UpdateCompensationAndBonusCommand>
    {
        private readonly ICompensationSalaryRepository _compensationSalaryRepository;
        private readonly IMapper _mapper;
        public UpdateCompensationAndBonusCommandHandeler(
            IMapper mapper, ICompensationSalaryRepository compensationSalaryRepository)
        {
            _mapper = mapper;
            _compensationSalaryRepository = compensationSalaryRepository;
        }
        public async Task<Unit> Handle(UpdateCompensationAndBonusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.TaskType == TaskType.Compensation)
                {
                    List<string> employees = new List<string>();
                    employees.Add(request.EmployeeIdentifier);
                    var c = (await _compensationSalaryRepository.GetCurrentCompensation(employees.ToArray())).ToList();
                    if(c.Count > 0)
                    {
                        var e = c.FirstOrDefault();
                        e.BaseSalary = request.BaseSalary;
                        e.EffectiveDate = request.EffectiveDate;
                        await _compensationSalaryRepository.SaveCompensation(e);
                    }                  
                }
                else if(request.TaskType == TaskType.Bonus)
                {
                    var salary = await _compensationSalaryRepository.GetCurrentSalary(request.EmployeeIdentifier);
                    if (salary == null)
                    {
                        List<string> employees = new List<string>();
                        employees.Add(request.EmployeeIdentifier);
                        var compensation = (await _compensationSalaryRepository.GetCurrentCompensation(employees.ToArray())).ToList();
                        if(compensation.Count > 0)
                        {
                            var s = _mapper.Map<Database.Entities.CompensationSalaries>(compensation.FirstOrDefault());
                            s.one_time_bonus = (request.one_time_bonus == 0) ? salary.one_time_bonus : request.one_time_bonus;
                            await _compensationSalaryRepository.SaveSalary(s);
                        }
                    }
                    else
                    {
                        salary.one_time_bonus = (request.one_time_bonus == 0) ? salary.one_time_bonus : request.one_time_bonus;
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
