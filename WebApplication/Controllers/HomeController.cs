using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using WebApplication.Business;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private IMessageService _messageService;
        private IChannelService _channelService;
        private IMessageSenderSevice _messageSenderSevice;

        public HomeController(IMessageSenderSevice messageSenderSevice, IMessageService messageService, IChannelService channelService)
        {
            _messageSenderSevice = messageSenderSevice;
            _messageService = messageService;
            _channelService = channelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateChannels()
        {
            await _channelService.Add(new Channel() {Name = "Technology"});
            await _channelService.Add(new Channel() {Name = "Sport"});
            await _channelService.Add(new Channel() {Name = "Chess"});
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChannelList()
        {
            return Ok(await _channelService.List());
        }
        public async Task<IActionResult> SendMessage(Message model)
        {
            await _messageService.Add(model);
            await _messageSenderSevice.SendMessage(model);
            return Ok();
        }
    }
}