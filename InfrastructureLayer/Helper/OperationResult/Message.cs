using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
