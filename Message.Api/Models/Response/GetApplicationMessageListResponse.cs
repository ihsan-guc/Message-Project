using Message.Data.Domain.Entities;
using System.Collections.Generic;

namespace Message.Api.Models.Response
{
    public class GetApplicationMessageListResponse : BaseResponse
    {
        public ICollection<ApplicationUser> ApplicationMessageList { get; set; }
    }
}
