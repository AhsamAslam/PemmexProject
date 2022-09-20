using AutoMapper;
using Compensation.API.Database.Interfaces;
using EventBus.Messages.Services;
using MediatR;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.SaveFunctionalBudget
{
    public class SaveFunctionalBudgetCommand : IRequest
    {
        public string organizationIdentifier { get; set; }
        public DateTime EffectiveDateTime { get; set; }
    }

    public class SaveFunctionalBudgetCommandHandeler : IRequestHandler<SaveFunctionalBudgetCommand>
    {
        private readonly IFunctionalBudgetRepository _context;
        private readonly ITopicPublisher _messagePublisher;
        public SaveFunctionalBudgetCommandHandeler(
            IFunctionalBudgetRepository context,
            ITopicPublisher messagePublisher)
        {
            _context = context;
            _messagePublisher = messagePublisher;
        }
        public async Task<Unit> Handle(SaveFunctionalBudgetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var functional_budgets = await _context.GetAll(request.organizationIdentifier);
                foreach(var b in functional_budgets)
                {
                    OrganizationUpdateEntity entity = new OrganizationUpdateEntity();
                    entity.EffectiveDate = request.EffectiveDateTime;
                    entity.EmployeeIdentifier = b.EmployeeIdentifier;
                    entity.Title = b.NewTitle;
                    entity.Grade = b.NewGrade;
                    entity.BaseSalary = b.BaseSalary;
                    entity.TaskType = PemmexCommonLibs.Domain.Enums.TaskType.Compensation;
                    await _messagePublisher.Publish<OrganizationUpdateEntity>(entity);

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
