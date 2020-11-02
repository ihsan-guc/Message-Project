using System;

namespace Message.Api.Models.Response
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid Id{ get; set; }
        public string Image { get; set; }
    }
}
