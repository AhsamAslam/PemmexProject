using MediatR;
using Notifications.API.Database.Repositories.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.Queries.GetUnReadNotificationsCount
{
    public class GetUnReadNotificationsCountQuery : IRequest<int>
    {
        public string Id { get; set; }
    }
    public class GetUnReadNotificationsCountQueryHandeler : IRequestHandler<GetUnReadNotificationsCountQuery, int>
    {
        private readonly Database.Repositories.Interface.INotification _context;

        public GetUnReadNotificationsCountQueryHandeler(Database.Repositories.Interface.INotification context)
        {
            _context = context;
        }
        public async Task<int> Handle(GetUnReadNotificationsCountQuery request, CancellationToken cancellationToken)
        {
            return await _context.CountUnReadNotifications(request.Id);
        }
    }
}
