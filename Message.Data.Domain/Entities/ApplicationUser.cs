using System;
using System.Collections.Generic;

namespace Message.Data.Domain.Entities
{
    public class ApplicationUser : BaseGuidEntity
    {
        public ApplicationUser()
        {
            SendingMessages = new HashSet<UserMessage>();
            ReceivingMessages = new HashSet<UserMessage>();
        }
        public Guid TokenId{ get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public Token Token { get; set; }
        public virtual ICollection<UserMessage> SendingMessages { get; set; }
        public virtual ICollection<UserMessage> ReceivingMessages { get; set; }
    }
}
