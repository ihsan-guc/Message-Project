using System;

namespace Message.Api.Models.Request
{
    public class GetUserMessageRequest
    {
        public Guid SenderApplicationUserId { get; set; }
        public Guid ReceiverApplicationUserId { get; set; }
    }
}
