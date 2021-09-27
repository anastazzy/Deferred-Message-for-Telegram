using System;
using System.Threading.Tasks;
using DMfT.Contracts;
using DMfT.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DMfT.App
{
    public class ServiceLoader : IServiceLoader
    {
        private readonly DMfTDbContext _dbContext;
        private readonly IMessageQueueService _service;

        public ServiceLoader(DMfTDbContext dbContext, IMessageQueueService service)
        {
            _dbContext = dbContext;
            _service = service;
        }
        public async Task LoadServiceAsync()
        {
            var messages = await _dbContext.Messages.ToArrayAsync();
            foreach (var message in messages)
            {
                await _service.ScheduleMessageAsync(message.Id);
            }
        }
    }
}
