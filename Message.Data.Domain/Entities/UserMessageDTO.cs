using System;

namespace Message.Data.Domain.Entities
{
    public class UserMessageDTO : BaseGuidEntity
    {
        public DateTime SendDate { get; set; }
        public string MessageText { get; set; }
    }
}
