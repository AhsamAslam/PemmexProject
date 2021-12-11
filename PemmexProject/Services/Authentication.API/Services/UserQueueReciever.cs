using Authentication.API.Commands.SaveUser;
using Authentication.API.Database.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Services
{
    public class UserQueueReciever : BackgroundService
    {
        private readonly AppSettings _appSettings;
        private IQueueClient _orderQueueClient;
        private readonly IMediator _mediator;

        public UserQueueReciever(IOptions<AppSettings> appSettings,IMediator mediator)
        {
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _mediator = mediator;
        }

        public async Task Handle(Message message, CancellationToken cancelToken)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var body = Encoding.UTF8.GetString(message.Body);
            var users  = JsonConvert.DeserializeObject<User>(body);
            var data = await _mediator.Send(new SaveUserCommand {  Users = users});
            Console.WriteLine($"Create Order Details are: {body}");
            await _orderQueueClient.CompleteAsync(message.SystemProperties.LockToken);
        }
        public virtual Task HandleFailureMessage(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            if (exceptionReceivedEventArgs == null)
                throw new ArgumentNullException(nameof(exceptionReceivedEventArgs));
            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageHandlerOptions = new MessageHandlerOptions(HandleFailureMessage)
            {
                MaxConcurrentCalls = 5,
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(10)
            };
            _orderQueueClient = new QueueClient(_appSettings.QueueConnectionString, _appSettings.QueueName);
            _orderQueueClient.RegisterMessageHandler(Handle, messageHandlerOptions);
            Console.WriteLine($"{nameof(UserQueueReciever)} service has started.");
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"{nameof(UserQueueReciever)} service has stopped.");
            await _orderQueueClient.CloseAsync();
        }

    }
}
