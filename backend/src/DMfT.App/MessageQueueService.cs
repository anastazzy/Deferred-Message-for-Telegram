using System;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using DMfT.Contracts;
using DMfT.DataAccess;
using DMfT.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DMfT.App
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly DMfTDbContext _dbContext;
        private readonly ITelegramSender _telegramSender;

        public MessageQueueService(DMfTDbContext dbContext, ITelegramSender telegramSender)
        {
            _dbContext = dbContext;
            _telegramSender = telegramSender;
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
            await ScheduleMessageAsync(message.Id);
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

        public async Task<bool> ScheduleMessageAsync(int id)
        {
            var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (message == null) return false;
            var startTime = message.StartTime;
            var sendIn =startTime - DateTimeOffset.Now;
            if (sendIn.TotalMilliseconds <= 0) return await _telegramSender.SendMessageAsync(id);
            _ = Task.Delay(sendIn).ContinueWith(_ => _telegramSender.SendMessageAsync(id));
            return true;
        }

       
    }
}
