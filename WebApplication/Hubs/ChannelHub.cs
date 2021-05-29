using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using WebApplication.Services;

namespace WebApplication.Hubs
{
    public class ChannelHub : Hub
    {
        private RedisSubscriber _redisSubscriber;

        public ChannelHub(RedisSubscriber redisSubscriber)
        {
            _redisSubscriber = redisSubscriber;
        }


        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var queryString = httpContext.Request.Query;
            StringValues channelIdValues;
            if (!queryString.TryGetValue("channelId", out channelIdValues))
            {
                throw new Exception("Kanal bilgisine ulaşılamadı.");
            }
            string channelId = channelIdValues.ToString();
            App.ChannelConnections.Add(channelId, Context.ConnectionId);
            await _redisSubscriber.SubscribeAsync(channelId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = Context.GetHttpContext();

            var queryString = httpContext.Request.Query;
            StringValues channelIdValues;
            if (!queryString.TryGetValue("channelId", out channelIdValues))
            {
                throw new Exception("Kanal bilgisine ulaşılamadı.");
            }
            string channelId = channelIdValues.ToString();
            
            App.ChannelConnections.Remove(channelId, Context.ConnectionId);
            
            await base.OnDisconnectedAsync(exception);
        }
    }
}