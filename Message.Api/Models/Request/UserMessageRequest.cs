using System;

namespace Message.Api.Models.Request
{
    public class UserMessageRequest
    {
        public Guid SenderApplicationUserId { get; set; }
        public Guid ReceiverApplicationUserId { get; set; }
        public string MessageText { get; set; }
    }
}
