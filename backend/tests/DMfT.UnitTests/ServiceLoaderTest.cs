using System;
using System.Threading.Tasks;
using DMfT.App;
using DMfT.Contracts;
using DMfT.DataAccess;
using DMfT.Domain;
using Moq;
using Xunit;

namespace DMfT.UnitTests
{
    public class ServiceLoaderTest : WithDataBaseTest
    {
        private readonly Mock<IMessageQueueService> _messageMock = new();
        private readonly ServiceLoader _service;
        
        public ServiceLoaderTest()
        {
            _service = new ServiceLoader(DbContext,_messageMock.Object);
        }
        [Fact]
        public async Task ServiceLoader_ShouldLoadService_AfterLaunchingTheApplication()
        {
            DbContext.Messages.Add(new Message
            {
                ChatId = 10,
                MessageText = "dih",
                Id = 1,
                StartTime = DateTimeOffset.Now,
            });
            await DbContext.SaveChangesAsync();
            await _service.LoadServiceAsync();
            _messageMock.Verify(x=> x.ScheduleMessageAsync(1));
        }
    }
}
