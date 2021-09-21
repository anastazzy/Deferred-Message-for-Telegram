using System;
using System.Linq;
using DMfT.Contracts;
using DMfT.DataAccess;
using DMfT.Domain;

namespace DMfT.App
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly DMfTDbContext _dbContext;
        public MessageQueueService(DMfTDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddMessage(MessageRequest messageRequest)
        {
            var message = new Message
            {
                ChatId = messageRequest.ChatId,
                MessageText = messageRequest.MessageText,
                StartTime = DateTimeOffset.Now.AddSeconds(messageRequest.DelayTime)
            };
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
            return message.Id;
        }

        public bool DeleteMessage(int id)
        {
            var message = _dbContext.Messages.FirstOrDefault(x => x.Id == id);
            if (message == null) return false;
            _dbContext.Messages.Remove(message);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
