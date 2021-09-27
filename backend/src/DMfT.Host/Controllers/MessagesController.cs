using System.Linq;
using System.Threading.Tasks;
using DMfT.Contracts;
using Microsoft.AspNetCore.Mvc;
using DMfT.DataAccess;
using DMfT.Domain;


namespace DMfT.Host.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageQueueService _service;


        public MessagesController(IMessageQueueService service)
        {
            _service = service;
        }

        // POST api/<MessagesController>
        [HttpPost]
        public Task<int> CreateMessage(MessageRequest messageRequest)
        {
            return _service.AddMessageAsync(messageRequest);
        }


        // DELETE api/<MessagesController>/5
        [HttpDelete("{id}")]
        public Task<bool> DeleteMessages(int id)
        {
            return _service.DeleteMessageAsync(id);
        }
    }
}
