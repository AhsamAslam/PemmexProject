using MediatR;
using Notifications.API.Database.Repositories.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.Commands.MarkNotification
{
    public class MarkNotificationCommand : IRequest
    {
        public string userId { get; set; }
    }

    public class MarkNotificationCommandHandeler : IRequestHandler<MarkNotificationCommand>
    {
        private readonly Database.Repositories.Interface.INotification _context;
        public MarkNotificationCommandHandeler(Database.Repositories.Interface.INotification context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(MarkNotificationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _context.UpdateNotificationToRead(request.userId);
                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
