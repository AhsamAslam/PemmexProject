using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Repositories.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.CreateBudgetCommand
{
    public class CreateBudgetCommand : IRequest
    {
        public string organizationIdentifier { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<BudgetDetail> budgetDetails { get; set; }
        public int TotalbudgetPercentage { get; set; }
        public double TotalbudgetValue { get; set; }
    }
    public class BudgetDetail
    {
        public string businessIdentifier { get; set; }
        public List<BudgetFunctions> budgetFunctions { get; set; }
        
    }
    public class BudgetFunctions
    {
        public int budgetPercentage { get; set; }
        public double budgetValue { get; set; }
        public JobFunction jobFunction { get; set; }
    }

    public class CreateBudgetCommandHandeler : IRequestHandler<CreateBudgetCommand>
    {
        
        private readonly IBudget _budget;
        private readonly IMapper _mapper;
        public CreateBudgetCommandHandeler(IBudget budget, IMapper mapper)
        {
            _budget = budget;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var budget = await _context.OrganizationBudgets
                //            .Where(o => o.organizationIdentifier == request.organizationIdentifier
                //            && o.startDate.Date == request.startDate.Date)
                //            .ToListAsync(cancellationToken);
                var budget = await _budget.GetOrganizationBudgetByOrganizationIdentifier(request.organizationIdentifier);
                if(budget.ToList().Count > 0)
                {
                    //_context.OrganizationBudgets.RemoveRange(budget);
                    //await _context.SaveChangesAsync(cancellationToken);
                    await _budget.DeleteOrganizationBudget(budget.ToList());
                }

                foreach(var b in request.budgetDetails)
                {
                    foreach(var d in b.budgetFunctions)
                    {
                        Database.Entities.OrganizationBudget org_buget = new Database.Entities.OrganizationBudget()
                        {
                            budgetPercentage = d.budgetPercentage,
                            budgetValue = d.budgetValue,
                            businessIdentifier = b.businessIdentifier,
                            endDate = request.endDate,
                            startDate = request.startDate,
                            jobFunction = d.jobFunction,
                            TotalbudgetPercentage = request.TotalbudgetPercentage,
                            TotalbudgetValue = request.TotalbudgetValue,
                            organizationIdentifier = request.organizationIdentifier
                        };
                        //_context.OrganizationBudgets.Add(org_buget);
                        await _budget.AddOrganizationBudget(org_buget);
                    }
                }
                //await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
