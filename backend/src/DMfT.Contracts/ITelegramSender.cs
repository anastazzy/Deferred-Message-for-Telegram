namespace DMfT.Contracts
{
    public interface ITelegramSender
    {
        bool SendMessage(int chatId, string textMessage);
    }
}
