using System;

namespace Message.Data.Domain.Entities
{
    public class UserMessageDTO : BaseGuidEntity
    {
        public DateTime SendDate { get; set; }
        public string MessageText { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }
}
