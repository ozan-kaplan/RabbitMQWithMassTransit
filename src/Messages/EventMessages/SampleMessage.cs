namespace Messages.EventMessages
{
    public class SampleMessage
    {
        public Guid MessageId { get; set; }
        public DateTime SentAt { get; set; }
        public string? Text { get; set; }

        public SampleMessage(Guid messageId, DateTime sentAt, string? text)
        {
            MessageId = messageId;
            SentAt = sentAt;
            Text = text;

        }
    }
}
