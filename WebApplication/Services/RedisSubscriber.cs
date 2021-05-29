using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;
using WebApplication.Hubs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class RedisSubscriber
    {
        private IHubContext<ChannelHub> _hubContext;
        private static ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect("localhost");

        public RedisSubscriber(IHubContext<ChannelHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SubscribeAsync(string channelId, string connectionId)
        {
            var pubsub = _connection.GetSubscriber();
            await pubsub.SubscribeAsync(channelId, async (channel, value) =>
            {
                if (!value.HasValue) return;
                var message = JsonConvert.DeserializeObject<Message>(value);
                await _hubContext.Clients.Client(connectionId)
                    .SendAsync("ReceiveMessage", message);
            });
        }
    }
}