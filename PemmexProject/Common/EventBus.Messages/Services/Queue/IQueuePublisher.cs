using System.Threading.Tasks;

namespace EventBus.Messages.Services
{
    public interface IQueuePublisher
    {
        public Task Publish<T>(T obj);
        public Task Publish(string obj);
    }
}