namespace InfrastructureLayer.Helper.OperationResult
{
    public class Message
    {
        public string MessageContent { get; set; }
        public OperationStatus MessageStatus { get; set; }

        public Message(string messageContent, OperationStatus messageStatus)
        {
            this.MessageContent = messageContent;
            this.MessageStatus = messageStatus;
        }
    }
}
