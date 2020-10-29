using Microsoft.AspNetCore.Http;
using System;

namespace Message.Api.Models.Request
{
    public class GetProfileRequest
    {
        public Guid UserId{ get; set; }
    }
}
