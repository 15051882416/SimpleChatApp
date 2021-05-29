using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApplication.Hubs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IMessageSenderSevice
    {
        Task SendMessage(Message model);
    }
}