using System;

namespace DMfT.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string MessageText { get; set; }
        public DateTimeOffset DelayTime { get; set; }
    }
}
