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
        /// Geçiş Mesaj Silme
        /// </summary>
        /// <param name="allMessageDelete"></param>
        /// Post: api/UserMessage/allMessageDelete
        [HttpPost]
        [Route("allMessageDelete")]
        public ActionResult AllMessageDelete(GetUserMessageRequest model)
        {
            if (ModelState.IsValid)
            {
                var MessageList = UnitOfWork.UserMessageRepository.ListMessage(model.SenderApplicationUserId, model.ReceiverApplicationUserId).ToList();
                foreach (var item in MessageList)
                {
                    UnitOfWork.UserMessageRepository.Delete(UnitOfWork.UserMessageRepository.GetById(item.Id));
                }
                UnitOfWork.Commit();
                return Ok(new BaseResponse { IsSuccess = true, Message = "Mesajlar Silindi." });
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
                return Ok(new BaseResponse { IsSuccess = true, Message = "Mesaj Gönderildi" });
            }
            return Ok(ReturnValidationError());
        }

        /// <summary>
        /// Kullanıcı Mesajlaştığı kişileri getirir.
        /// </summary>
        /// <param name="GetApplicationMessageList"></param>
        /// Post: api/UserMessage/GetApplicationMessageList
        [HttpPost]
        [Route("GetApplicationMessageList")]
        public ActionResult GetApplicationMessageList(GetApplicationMessageListRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = new GetApplicationMessageListResponse
                {
                    IsSuccess = true,
                    Message = "Başarılı",
                    ApplicationMessageList = UnitOfWork.UserMessageRepository.ApplicationMessageList(model.ApplicationUserId).ToList()
                };
                return Ok(response);
            }
            return Ok(ReturnValidationError());
        }
    }
}
