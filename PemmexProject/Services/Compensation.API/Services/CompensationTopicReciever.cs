using Compensation.API.Commands.SaveCompensation;
using Compensation.API.Commands.SaveFunctionalBudgetByWorkFlow;
using Compensation.API.Commands.UpdateCompensationAndBonus;
using Compensation.API.Dtos;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Services
{
    public class CompensationTopicReciever : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IMediator _mediator;
        public CompensationTopicReciever(ISubscriptionClient subscriptionClient, IMediator mediator)
        {
            _subscriptionClient = subscriptionClient;
            _mediator = mediator;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler(async (message,token) =>
            {
                string response = Encoding.UTF8.GetString(message.Body);
                var a = message.UserProperties.FirstOrDefault();
                if (a.Value != null && (a.Value.ToString() == nameof(CompensationEntity)))
                {
                    var compensation = JsonConvert.DeserializeObject<CompensationDto>(response);
                    var data = await _mediator.Send(new SaveCompensationCommand { compensationDto = compensation });
                }
                else if(a.Value.ToString() == nameof(OrganizationUpdateEntity))
                {
                    var compensation = JsonConvert.DeserializeObject<UpdateCompensationAndBonusCommand>(response);
                    var data = await _mediator.Send(compensation);
                }
                else if (a.Value.ToString() == nameof(BudgetPromotionUpdateEntity))
                {
                    var compensation = JsonConvert.DeserializeObject<SaveFunctionalBudgetByWorkFlowCommand>(response);
                    var data = await _mediator.Send(compensation);
                }
                _ = _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            },new MessageHandlerOptions(ExceptionReceivedHandler) { 
                
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
            return Task.CompletedTask;
        }
        protected Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Exception:: {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
