namespace Message.Api.Models.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
