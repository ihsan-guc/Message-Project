using System.Collections.Generic;
using static Message.Data.Domain.Enums.DbEnum;

namespace Message.Api.Models.Response
{
    public class BooleanResponse : BaseResponse
    {
        public bool IsValid { get; set; }
    }
    public class BaseResponse
    {
        public BaseResponse()
        {
            ErrorMessage = new Dictionary<string, string>();
        }
        public bool IsSuccess { get; set; }
        public ErrorType Type { get; set; }
        public string ErrorCode { get; set; }
        public Dictionary<string, string> ErrorMessage { get; set; }
        public string Message { get; set; }
    }
}
