using System.Threading.Tasks;

namespace DMfT.Contracts
{
    public interface IMessageQueueService
    {
        Task<int> AddMessageAsync(MessageRequest messageRequest);
        Task<bool> DeleteMessageAsync(int id);
    }
}
