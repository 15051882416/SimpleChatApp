using System;

namespace WebApplication.Models
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SenderNickName { get; set; }
        public string ChannelId { get; set; }
        public string Text { get; set; }
    }
}