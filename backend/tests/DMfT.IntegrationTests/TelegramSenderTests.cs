using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DMfT.App;
using DMfT.Contracts;
using DMfT.Domain;
using DMfT.UnitTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DMfT.IntegrationTests
{
    public class TelegramSenderTests : WithDataBaseTest
    {
        private readonly TelegramSender _telegramSender;
        public TelegramSenderTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(DbContext);
            serviceCollection.AddSingleton(new HttpClient());
            _telegramSender = new TelegramSender(serviceCollection.BuildServiceProvider(), Options.Create(new TelegramBotOptions
            {
                SecretKey = "1999928393:AAHCT03DRZuVjddJVFyZB-Z5WprNzHL_x88",
            }), new Mock<ILogger<TelegramSender>>().Object);
        }

        [Fact]
        public async Task SendMessage_ShouldSendMessageInTheChat()
        {
            DbContext.Messages.Add(new Message
            {
                ChatId = 381835498,
                Id = 30,
                MessageText = "dkwh",
                StartTime = DateTimeOffset.Now,
            });
            await DbContext.SaveChangesAsync();
            var response = await _telegramSender.SendMessageAsync(30);
            Assert.True(response);
            var result = DbContext.Messages.FirstOrDefault(x => x.Id == 30);
            Assert.Null(result);
        }
    }
}
