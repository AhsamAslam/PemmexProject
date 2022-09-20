using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PemmexTestingApp
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://pemmex-queues.servicebus.windows.net/;SharedAccessKeyName=pemmexSharedAccessKey;SharedAccessKey=BOXYaEcCQO5bE2QzfHoQvK10M7u+nwZ+DE2nIYc4WMc=";
        // name of the topic created on the Azure portal
        const string TopicName = "pemmex-topic";

        static ITopicClient topicClient;

        // the following parameters for receving messages
        const string SubscriptionName = "taskUpdation-subscription/$deadletterqueue";
        //const string SubscriptionName = "user-subscription";
        //const string SubscriptionName = "user-subscription";

        static ISubscriptionClient subscriptionClient;

        static void Main(string[] args)
        {

            ClearingDeadLetterAsync().GetAwaiter().GetResult();
        }
        static async Task ClearingDeadLetterAsync()
        {

            subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
            RegisterOnMessageHandlerAndReceiveMessages();

            Console.ReadKey();
            await subscriptionClient.CloseAsync();

        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false

            };

            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: SequenceNumber: {message.SystemProperties.SequenceNumber} Body: {Encoding.UTF8.GetString(message.Body)}");

            //complete the message to complete once received
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");

            Console.WriteLine($" - Endpoint: {context.Endpoint}");
            Console.WriteLine($" - Entry path: {context.EntityPath}");
            Console.WriteLine($" - Executing Action: {context.Action}");

            return Task.CompletedTask;
        }
        static async Task Fun1()
        {
            try
            {
                var builder = "Endpoint=sb://pemmex-queues.servicebus.windows.net/;SharedAccessKeyName=pemmexSharedAccessKey;SharedAccessKey=BOXYaEcCQO5bE2QzfHoQvK10M7u+nwZ+DE2nIYc4WMc=";
                string TopicName = "pemmex-topic";
                var messageReceiver = new MessageReceiver(builder, EntityNameHelper.FormatSubscriptionPath(TopicName, "holiday-subscription/$DeadLetterQueue"), ReceiveMode.PeekLock);
                var message = await messageReceiver.ReceiveAsync();
                while ((message = await messageReceiver.ReceiveAsync()) != null)
                {
                    await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
                }
                await messageReceiver.CloseAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static async Task DeleteDeadLetterMessageAsync()
        {
            //var builder = "Endpoint=sb://pemmex-queues.servicebus.windows.net/;SharedAccessKeyName=pemmexSharedAccessKey;SharedAccessKey=BOXYaEcCQO5bE2QzfHoQvK10M7u+nwZ+DE2nIYc4WMc=";
            //string deadLetterQueueName = "Pemmex-Queues/pemmex-topic/$DeadLetterQueue";
            //QueueClient client = QueueClient.CreateFromConnectionString(builder,
            //        deadLetterQueueName, ReceiveMode.PeekLock);
            //while (client.Receive() != null)
            //{
            //    var receivedMessage = client.Receive();
            //    //do something with the message here
            //    receivedMessage?.Complete();
            //}
        }
    }
}
