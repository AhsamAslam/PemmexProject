using PemmexAPIAggregator.Models;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface INotificationService
    {
        Task<string> PostNotification(string notification);
        Task<string> PostAnnounceNotification(string notification);
    }
    public class NotificationService: INotificationService
    {
        private readonly HttpClient _client;
        public NotificationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> PostAnnounceNotification(string notification)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/Notifications/AnnounceNotification"),
                    Content = new StringContent(notification, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                //string data = JsonSerializer.Deserialize<string>(result);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> PostNotification(string command)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/Notifications"),
                    Content = new StringContent(command, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                //string data = JsonSerializer.Deserialize<string>(result);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
