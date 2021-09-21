using System;
using DMfT.App;
using DMfT.Contracts;
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
        public void AddMessage_ShouldAddNewMessageInDataBase()
        {
            // Arrange
            var request = new MessageRequest
            {
                ChatId = 10,
                MessageText = "Hello World!",
                DelayTime = 100,
            };

            // Act
            _service.AddMessage(request);

            // Assert
            var message = Assert.Single(DbContext.Messages);
            Assert.Equal(10, message.ChatId);
            Assert.Equal("Hello World!", message.MessageText);
            Assert.Equal(DateTimeOffset.Now.AddSeconds(100).DateTime, message.StartTime.DateTime, new TimeSpan(0, 0, 2));
        }

        [Fact]
        public void DeleteMessage_ShouldDeleteMessageFromDataBase_IfItExists()
        {

        }

        [Fact]
        public void DeleteMessage_ShouldNotDeleteMessageFromDataBase_IfItNotExists()
        {

        }
    }
}
