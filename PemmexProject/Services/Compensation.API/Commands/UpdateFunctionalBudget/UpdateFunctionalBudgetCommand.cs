using Compensation.API.Database.context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.UpdateFunctionalBudget
{
    public class UpdateFunctionalBudgetCommand : IRequest<int>
    {
        public string EmployeeIdentifier { get; set; }
        public string CurrentGrade { get; set; }
        public string NewGrade { get; set; }
        public double BaseSalary { get; set; }
        public double TotalCurrentSalary { get; set; }
        public double NewBaseSalary { get; set; }
        public double NewTotalSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    public class UpdateFunctionalBudgetCommandHandeler : IRequestHandler<UpdateFunctionalBudgetCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public UpdateFunctionalBudgetCommandHandeler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateFunctionalBudgetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var budget = await _context.FunctionalBudgets.Where(f => f.EmployeeIdentifier == request.EmployeeIdentifier
                   && f.isActive == true).FirstOrDefaultAsync();
                budget.EffectiveDate = request.EffectiveDate;
                budget.BaseSalary = request.BaseSalary;
                budget.NewBaseSalary = request.NewBaseSalary;
                budget.NewTotalSalary = request.NewTotalSalary;
                budget.TotalCurrentSalary = request.TotalCurrentSalary;
                budget.CurrentGrade = request.CurrentGrade;
                budget.NewGrade = request.NewGrade;

                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
