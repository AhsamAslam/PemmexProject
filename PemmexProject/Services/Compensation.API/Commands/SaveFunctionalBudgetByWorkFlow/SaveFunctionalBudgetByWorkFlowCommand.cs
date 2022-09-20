using AutoMapper;
using Compensation.API.Database.Entities;
using Compensation.API.Database.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.SaveFunctionalBudgetByWorkFlow
{
    public class SaveFunctionalBudgetByWorkFlowCommand : IRequest<int>
    {
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentTitle { get; set; }
        public string NewTitle { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public string CurrentGrade { get; set; }
        public string NewGrade { get; set; }
        public string JobFunction { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double TotalCurrentSalary { get; set; }
        public double mandatoryPercentage { get; set; }
        public double IncreaseInPercentage { get; set; }
        public double NewBaseSalary { get; set; }
        public double NewTotalSalary { get; set; }
        public string currencyCode { get; set; }
        public double IncreaseInCurrency { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }

    public class SaveFunctionalBudgetByWorkFlowCommandHandeler : IRequestHandler<SaveFunctionalBudgetByWorkFlowCommand, int>
    {
        private readonly IFunctionalBudgetRepository _context;
        private readonly IMapper _mapper;
        public SaveFunctionalBudgetByWorkFlowCommandHandeler(IFunctionalBudgetRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(SaveFunctionalBudgetByWorkFlowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                try
                {
                    var budget = _mapper.Map<FunctionalBudget>(request);
                    return await _context.Save(budget);
                }
                catch (Exception e)
                {
                    throw new Exception(e.ToString());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
