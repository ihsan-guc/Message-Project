using Message.Api.Core;
using Message.Api.Models.Request;
using Message.Api.Models.Response;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Message.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessageController : BaseApiController
    {
        ITokenGenerator TokenGenerator;
        public UserMessageController(ITokenGenerator tokenGenerator)
        {
            TokenGenerator = tokenGenerator;
        }
        /// <summary>
        /// Mesaj List Getirme
        /// </summary>
        /// <param name="GetListMessage"></param>
        /// Post: api/UserMessage/getListMessage
        [HttpPost]
        [Route("getListMessage")]
        public ActionResult GetListMessage(GetUserMessageRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = new GetListMessageResponse
                {
                    IsSuccess = true,
                    MessageList = UnitOfWork.UserMessageRepository.ListMessage(model.SenderApplicationUserId, model.ReceiverApplicationUserId).ToList()
                };
                return Ok(response);
            }
            return Ok(ReturnValidationError());
        }

        /// <summary>
        /// Mesaj Gönderme
        /// </summary>
        /// <param name="ChatMessage"></param>
        /// Post: api/UserMessage/chatMessage
        [HttpPost]
        [Route("chatMessage")]
        public ActionResult ChatMessage(UserMessageRequest model)
        {
            if (ModelState.IsValid)
            {
                var message = new UserMessage()
                {
                    Id = Guid.NewGuid(),
                    MessageText = model.MessageText,
                    SendDate = DateTime.Now,
                    ReceiverApplicationUserId = model.ReceiverApplicationUserId,
                    SenderApplicationUserId = model.SenderApplicationUserId
                };
                UnitOfWork.UserMessageRepository.Add(message);
                UnitOfWork.Commit();
                var messageresponse = new MessageResponse
                {
                    IsSuccess = true,
                    Message = "Mesaj Gönderildi",
                    Datetime = message.SendDate,
                    //MessageText = message.MessageText,
                    ReceiverApplicationUserId = message.ReceiverApplicationUserId,
                    SenderApplicationUserId = message.SenderApplicationUserId,
                };
                var Receivermessagelist = UnitOfWork.UserMessageRepository.GetQueryable().Where(p => p.ReceiverApplicationUserId == message.ReceiverApplicationUserId);
                foreach (var item in Receivermessagelist)
                {
                    messageresponse.MessageText.Add(item.MessageText);
                }
                return Ok(messageresponse);
            }
            return Ok(ReturnValidationError());
        }



    }
}
