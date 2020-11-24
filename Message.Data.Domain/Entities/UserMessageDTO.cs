using System;

namespace Message.Data.Domain.Entities
{
    public class UserMessageDTO : BaseGuidEntity
    {
        public DateTime SendDate { get; set; }
        public string MessageText { get; set; }
        public Guid MessageSender { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public string SenderImage { get; set; }
        public Guid ReceiverId { get; set; }
        public string ReceiverUserName { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ReceiverImage { get; set; }
    }
}
