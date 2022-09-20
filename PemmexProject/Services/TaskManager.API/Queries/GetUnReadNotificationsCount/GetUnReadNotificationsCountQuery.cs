using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.NotificationHub;

namespace TaskManager.API.Queries.GetUnReadNotificationsCount
{
    public class GetUnReadNotificationsCountQuery : IRequest<int>
    {
        public string Id { get; set; }
    }
    public class GetUnReadNotificationsCountQueryHandeler : IRequestHandler<GetUnReadNotificationsCountQuery, int>
    {
        private readonly INotificationRepository _context;

        public GetUnReadNotificationsCountQueryHandeler(INotificationRepository context)
        {
            _context = context;
        }
        public async Task<int> Handle(GetUnReadNotificationsCountQuery request, CancellationToken cancellationToken)
        {
            return await _context.CountUnReadNotifications(request.Id);
        }
    }
}
