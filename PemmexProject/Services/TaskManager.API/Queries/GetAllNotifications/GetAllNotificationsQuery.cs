using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Dtos;
using TaskManager.API.NotificationHub;

namespace TaskManager.API.Queries.GetAllNotifications
{
    public class GetAllNotificationsQuery : IRequest<List<NotificationDto>>
    {
        public string Id { get; set; }
    }
    public class GetAllNotificationsQueryHandeler : IRequestHandler<GetAllNotificationsQuery, List<NotificationDto>>
    {
        private readonly INotificationRepository _context;
        private readonly IMapper _mapper;

        public GetAllNotificationsQueryHandeler(INotificationRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _context.GetAllNotification(request.Id);
            return _mapper.Map<List<Notifications>, List<NotificationDto>>(notifications);
        }
    }
}
