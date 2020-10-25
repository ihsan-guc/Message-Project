using Message.Api.Models.Response;
using Message.Data.DAL;
using Message.Data.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Message.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {

        private MessageContext messageContext;

        public MessageContext MessageContext
        {
            get { return messageContext; }
            set { messageContext = value; }
        }
        [NonAction]
        public BaseResponse ReturnValidationError()
        {
            var response = new BaseResponse
            {
                IsSuccess = false,
                Type = DbEnum.ErrorType.Validation,
            };
            if (!ModelState.IsValid)
            {
                StringBuilder errors;

                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        errors = new StringBuilder();

                        foreach (var err in state.Value.Errors)
                        {
                            errors.AppendLine(err.ErrorMessage);
                        }

                        response.ErrorMessage.Add(state.Key, errors.ToString());
                    }
                }
            }
            return response;
        }
    }
}
