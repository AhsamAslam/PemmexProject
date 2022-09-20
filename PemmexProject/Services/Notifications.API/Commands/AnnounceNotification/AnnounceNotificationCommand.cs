using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Notifications.API.Dtos;
using Notifications.API.NotificationHub;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.Commands.AnnounceNotification
{
    public class AnnounceNotificationCommand : MediatR.IRequest
    {
        public List<string> userId { get; set; }
        public bool isRead { get; set; }
        public TaskType taskType { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }

    public class AnnounceNotificationCommandHandeler : IRequestHandler<AnnounceNotificationCommand>
    {
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public AnnounceNotificationCommandHandeler(IHubContext<NotificationUserHub> notificationUserHubContext,
            IUserConnectionManager userConnectionManager,
            INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AnnounceNotificationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<AnnounceNotificationDto> announceNotificationList = new List<AnnounceNotificationDto>();
                foreach (var item in request.userId)
                {
                    AnnounceNotificationDto obj = new AnnounceNotificationDto();
                    obj.EmployeeId = item;
                    obj.title = request.title;
                    obj.description = request.description;
                    obj.isRead = false;
                    obj.tasks = 0;

                    announceNotificationList.Add(obj);
                }
                //var n = _mapper.Map<AnnounceNotificationDto>(request);

                await _notificationRepository.AddAnnounceNotification(announceNotificationList);
                //Parallel.ForEach(announceNotificationList, async notification =>
                //{
                //    var connections =  _userConnectionManager.GetUserConnections(notification.EmployeeId.ToString());
                //    if (connections != null && connections.Count > 0)
                //    {
                //        foreach (var connectionId in connections)
                //        {
                //            await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("New Message", notification.title, notification.description, await _notificationRepository.CountUnReadNotifications(notification.EmployeeId));
                //        }
                //    }

                //});
               

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
