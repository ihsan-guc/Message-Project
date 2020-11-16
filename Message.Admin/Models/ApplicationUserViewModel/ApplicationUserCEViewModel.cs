using Microsoft.AspNetCore.Http;

namespace Message.Admin.Models.ApplicaitonUserViewModel
{
    public class ApplicationUserCEViewModel : BaseCEViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }

    }
}
