using Microsoft.AspNetCore.Http;
using System;

namespace Message.Api.Models.Request
{
    public class UpdateProfilePhotoRequest
    {
        public Guid Id{ get; set; }
        public IFormFile File{ get; set; }
    }
}
