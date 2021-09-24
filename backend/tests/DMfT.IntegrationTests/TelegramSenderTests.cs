using System.Net.Http;
using System.Threading.Tasks;
using DMfT.App;
using Xunit;

namespace DMfT.IntegrationTests
{
    public class TelegramSenderTests
    {
        private readonly TelegramSender _telegramSender;
        public TelegramSenderTests()
        {
            _telegramSender = new TelegramSender(new HttpClient(), new TelegramBotOptions
            {
                SecretKey ="",
            });
        }

        [Fact]
        public Task<bool> SendMessage_ShouldSendMessageInTheChat()
        {
            var response = _telegramSender.SendMessageAsync(381835498, "Hello, baby");
            return response;
        }
    }
}
