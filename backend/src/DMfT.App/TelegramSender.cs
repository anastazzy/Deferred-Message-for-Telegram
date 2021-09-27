using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DMfT.Contracts;
using DMfT.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DMfT.App
{
    public class TelegramSender : ITelegramSender
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TelegramSender> _logger;
        private readonly TelegramBotOptions _options;
        private const string Api = "https://api.telegram.org/bot{0}/sendMessage?text={1}&chat_id={2}&parse_mode=html";

        public TelegramSender(IServiceProvider serviceProvider, IOptions<TelegramBotOptions> options, ILogger<TelegramSender> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<bool> SendMessageAsync(HttpClient httpClient, int chatId, string textMessage)
        {
            var response = await httpClient.GetAsync(String.Format(Api, _options.SecretKey, textMessage, chatId));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendMessageAsync(int id)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DMfTDbContext>();
                var message = dbContext.Messages.FirstOrDefault(x => x.Id == id);
                if (message == null) return false;
                var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
                var result = await SendMessageAsync(httpClient,message.ChatId, message.MessageText);
                if (result == false)
                {
                    _logger.LogWarning("Message was not sends");
                }; 
                dbContext.Remove(message);
                await dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Message was not sends");
                throw;
            }
        }
    }
}
