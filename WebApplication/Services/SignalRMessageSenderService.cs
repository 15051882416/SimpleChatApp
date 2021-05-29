using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;
using WebApplication.Business;
using WebApplication.Hubs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class MessageSenderManager : IMessageSenderSevice
    {
        // private readonly IHubContext<ChannelHub> _chatHubContext;
        private ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost");

        public async Task SendMessage(Message model)
        {
            var pubsub = connection.GetSubscriber();
            await pubsub.PublishAsync(model.ChannelId, JsonConvert.SerializeObject(model));
            // await _chatHubContext.Clients
            //     .Clients(App.ChannelConnections.GetConnections(model.ChannelId))
            //     .SendAsync("ReceiveMessage", new {message, nickname});
        }
    }
}