using System;

namespace Message.Data.Domain.Entities
{
    public class ApplicationUser : BaseGuidEntity
    {
        public Guid? TokenId{ get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public Token Token { get; set; }
    }
}
