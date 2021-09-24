using System.Threading.Tasks;

namespace DMfT.Contracts
{
    public interface ITelegramSender
    {
        Task<bool> SendMessageAsync(int chatId, string textMessage);
    }
}
