using System;

namespace Message.Data.Domain.Entities
{
    public class Token : BaseGuidEntity
    {
        public Guid ApplicationUserId { get; set; }
        public string TokenString { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
