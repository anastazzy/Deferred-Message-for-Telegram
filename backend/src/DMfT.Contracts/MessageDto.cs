using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMfT.App
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string MessageText { get; set; }
        public DateTimeOffset StartTime { get; set; }
    }
}
