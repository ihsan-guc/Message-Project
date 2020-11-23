using System;

namespace Message.Api.Models.Request
{
    public class GetApplicationMessageListRequest
    {
        public Guid ApplicationUserId { get; set; }
    }
}
