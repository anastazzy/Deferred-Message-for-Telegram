using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<int> AddMessageAsync(MessageRequest messageRequest)
        {
            var message = new Message
            {
                ChatId = messageRequest.ChatId,
                MessageText = messageRequest.MessageText,
                StartTime = DateTimeOffset.Now.AddSeconds(messageRequest.DelayTime)
            };
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
            return message.Id;
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            var message = _dbContext.Messages.FirstOrDefault(x => x.Id == id);
            if (message == null) return false;
            _dbContext.Messages.Remove(message);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
