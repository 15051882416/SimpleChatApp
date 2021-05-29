using WebApplication.Models;

namespace WebApplication
{
    public static class App
    {
        public static SignalRConnectionMapping<string> ChannelConnections { get; set; } = new SignalRConnectionMapping<string>();
    }
}