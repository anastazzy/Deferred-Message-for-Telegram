namespace DMfT.Contracts
{
    public class MessageRequest
    {
        public int ChatId { get; set; }
        public string MessageText { get; set; }
        public long DelayTime { get; set; }
    }
}
