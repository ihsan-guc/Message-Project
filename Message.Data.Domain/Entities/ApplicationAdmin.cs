using System;

namespace Message.Data.Domain.Entities
{
    public class ApplicationAdmin : BaseGuidEntity
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
