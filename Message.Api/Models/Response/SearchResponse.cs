using Message.Data.Domain.Entities;
using System.Collections.Generic;

namespace Message.Api.Models.Response
{
    public class SearchResponse : BaseResponse
    {
        public SearchResponse()
        {
            ApplicationUsersList = new List<ApplicationUser>();
        }
        public List<ApplicationUser> ApplicationUsersList{ get; set; }
    }
}
