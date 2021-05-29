using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Dal;
using WebApplication.Models;

namespace WebApplication.Business
{
    public interface IChannelService
    {
        Task<List<Channel>> List();
        Task Add(Channel model);
    }

    public class ChannelManager : IChannelService
    {
        private ChannelDal _channelDal;

        public ChannelManager(ChannelDal channelDal)
        {
            _channelDal = channelDal;
        }

        public async Task<List<Channel>> List()
        {
            return await _channelDal.Get();
        }

        public async Task Add(Channel model)
        {
            await _channelDal.Create(model);
        }
    }

    public interface IMessageService
    {
        Task Add(Message model);
    }

    public class MessageManager : IMessageService
    {
        private MessageDal _messageDal;

        public MessageManager(MessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public async Task Add(Message model)
        {
            await _messageDal.Create(model);
        }
    }
}