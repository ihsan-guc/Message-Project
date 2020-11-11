using Message.Data.Domain.Entities;
using System.Collections.Generic;

namespace Message.Api.Models.Response
{
    public class GetListMessageResponse : BaseResponse
    {
        public ICollection<UserMessageDTO> MessageList { get; set; }
    }
}
