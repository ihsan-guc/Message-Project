using System;

namespace Message.Data.Domain.Entities
{
    public class UserMessage : BaseGuidEntity
    {
        public Guid SenderApplicationUserId { get; set; }
        public Guid ReceiverApplicationUserId { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }
        public virtual ApplicationUser SenderUser { get; set; }
        public virtual ApplicationUser ReceiverUser { get; set; }
    }
}
