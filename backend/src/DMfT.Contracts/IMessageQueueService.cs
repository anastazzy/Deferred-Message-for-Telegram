using System.Threading.Tasks;
using DMfT.App;

namespace DMfT.Contracts
{
    public interface IMessageQueueService
    {
        Task <int> AddMessageAsync(MessageRequest messageRequest);
        Task <bool> DeleteMessageAsync(int id);
        Task <bool> ScheduleMessageAsync(int id);
        Task <MessageDto[]> GetMessagesList();
    }
}
