using System;
using System.Collections.Generic;

namespace Message.Api.Models.Response
{
    public class MessageResponse : BaseResponse
    {
        public MessageResponse()
        {
            MessageText = new List<string>();
        }
        public Guid SenderApplicationUserId { get; set; }
        public Guid ReceiverApplicationUserId { get; set; }
        public DateTime Datetime { get; set; }
        public List<string> MessageText { get; set; }
    }
}
