using System;
using System.Threading.Tasks;
using DMfT.App;
using DMfT.Contracts;
using DMfT.Domain;
using Xunit;

namespace DMfT.UnitTests
{
    public class MessageQueueServiceTests : WithDataBaseTest
    {
        private readonly MessageQueueService _service;

        public MessageQueueServiceTests()
        {
            _service = new MessageQueueService(DbContext);
        }

        [Fact]
        public async Task AddMessage_ShouldAddNewMessageInDataBase()
        {
            // Arrange
            var request = new MessageRequest
            {
                ChatId = 10,
                MessageText = "Hello World!",
                DelayTime = 100,
            };

            // Act
            await _service.AddMessageAsync(request);

            // Assert
            var message = Assert.Single(DbContext.Messages);
            Assert.Equal(10, message.ChatId);
            Assert.Equal("Hello World!", message.MessageText);
            Assert.Equal(DateTimeOffset.Now.AddSeconds(100).DateTime, message.StartTime.DateTime, new TimeSpan(0, 0, 2));
        }

        [Fact]
        public async Task DeleteMessage_ShouldDeleteMessageFromDataBase_IfItExists()
        {
            // Arrange
            var request = new Message
            {
                ChatId = 10,
                MessageText = "Hello World!",
                StartTime = DateTimeOffset.Now,
            };
            DbContext.Messages.Add(request);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _service.DeleteMessageAsync(request.Id);

            // Assert
            Assert.True(result);
            Assert.Empty(DbContext.Messages);
        }

        [Fact]
        public async Task DeleteMessage_ShouldNotDeleteMessageFromDataBase_IfItNotExists()
        {
            // Arrange
            var idMessage = 10;

            // Act
            var result = await _service.DeleteMessageAsync(idMessage);

            // Assert
            Assert.False(result);
        }
    }
}
