using System;
using System.Net.Http;
using System.Threading.Tasks;
using DMfT.Contracts;

namespace DMfT.App
{
    public class TelegramSender : ITelegramSender
    {
        private readonly HttpClient _httpClient;
        private readonly TelegramBotOptions _options;
        private const string Api = "https://api.telegram.org/bot{0}/sendMessage?text={1}&chat_id={2}&parse_mode=html";

        public TelegramSender(HttpClient httpClient, TelegramBotOptions options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<bool> SendMessageAsync(int chatId, string textMessage)
        {
            var response = await _httpClient.GetAsync(String.Format(Api, _options.SecretKey, textMessage, chatId));
            return response.IsSuccessStatusCode;
        }
    }
}
