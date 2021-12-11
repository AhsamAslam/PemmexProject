using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Organization.API.Commands.UpdateOrganization;
using Organization.API.Commands.UpdateTaskToOrganizationCommand;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Services
{
    public class TaskTopicReciever : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IMediator _mediator;
        public TaskTopicReciever(ISubscriptionClient subscriptionClient, IMediator mediator)
        {            _subscriptionClient = subscriptionClient;
            _mediator = mediator;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler(async (message,token) =>
            {
                string response = Encoding.UTF8.GetString(message.Body);
                var a = message.UserProperties.FirstOrDefault();
                if (a.Value != null && a.Value.ToString() == nameof(OrganizationUpdateEntity))
                {
                    var titlecommand = JsonConvert.DeserializeObject<UpdateOrganizationCommand>(response);
                    var data = await _mediator.Send(titlecommand);
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
