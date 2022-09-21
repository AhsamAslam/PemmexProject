using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Notifications.API.NotificationHub;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.Commands.SaveNotification
{
    public class SaveNotificationCommand : MediatR.IRequest
    {
        public string userId { get; set; }
        public bool isRead { get; set; }
        public TaskType taskType { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }

    public class SaveNotificationCommandHandeler : IRequestHandler<SaveNotificationCommand>
    {
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public SaveNotificationCommandHandeler(IHubContext<NotificationUserHub> notificationUserHubContext,
            IUserConnectionManager userConnectionManager,
            INotificationRepository notificationRepository,IMapper mapper)
        {
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(SaveNotificationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var n = _mapper.Map<Database.Entities.Notifications>(request);
                await _notificationRepository.AddNotifications(n);
                var connections = _userConnectionManager.GetUserConnections(n.EmployeeId.ToString());
                if (connections != null && connections.Count > 0)
                {
                    foreach (var connectionId in connections)
                    {
                        await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("New Message",n.title, n.description, await _notificationRepository.CountUnReadNotifications(n.EmployeeId));
                    }
                }

                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
