namespace DMfT.Contracts
{
    public interface IMessageQueueService
    {
        int AddMessage(MessageRequest messageRequest);
        bool DeleteMessage(int id);
    }
}
