using System.Threading.Tasks;
using PemmexCommonLibs.Domain.Common;

namespace PemmexCommonLibs.Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
