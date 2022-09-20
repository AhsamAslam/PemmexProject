using AutoMapper;
using MediatR;
using Notifications.API.Database.Repositories.Interface;
using Notifications.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.Queries.GetAllNotifications
{
    public class GetAllNotificationsQuery : IRequest<List<NotificationDto>>
    {
        public string Id { get; set; }
    }
    public class GetAllNotificationsQueryHandeler : IRequestHandler<GetAllNotificationsQuery, List<NotificationDto>>
    {
        private readonly Database.Repositories.Interface.INotification _context;
        private readonly IMapper _mapper;

        public GetAllNotificationsQueryHandeler(Database.Repositories.Interface.INotification context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var notifications = await _context.GetAllNotification(request.Id);
                return _mapper.Map<List<Database.Entities.Notifications>, List<NotificationDto>>(notifications);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
