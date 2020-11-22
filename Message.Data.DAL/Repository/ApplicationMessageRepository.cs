using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Message.Data.DAL.Repository
{
    public interface IUserMessageRepository : IRepository<UserMessage>
    {
        List<UserMessageDTO> ListMessage(Guid SenderId, Guid ReceiverId);
    }
    public class UserMessageRepository : EfRepository<UserMessage>, IUserMessageRepository
    {
        public UserMessageRepository(MessageContext context) : base(context)
        {

        }
        public List<UserMessageDTO> ListMessage(Guid SenderId, Guid ReceiverId)
        {
            var userMessageDTO = new List<UserMessageDTO>();
            var userMessagelist = _context.UserMessages.Where(p => (p.SenderApplicationUserId == SenderId && p.ReceiverApplicationUserId == ReceiverId) || (p.SenderApplicationUserId == ReceiverId && p.ReceiverApplicationUserId == SenderId)).OrderBy(s => s.SendDate);
            var senderUser = _context.ApplicationUsers.Where(p => p.Id == SenderId).FirstOrDefault();
            var receiverUser = _context.ApplicationUsers.Where(c => c.Id == ReceiverId).FirstOrDefault();
            string k = receiverUser.UserName;
            foreach (var message in userMessagelist)
            {
                var usermessage = new UserMessageDTO()
                {
                    Id = message.Id,
                    MessageText = message.MessageText,
                    SendDate = message.SendDate,
                    SenderFirstName = senderUser.FirstName,
                    SenderLastName = senderUser.LastName,
                    SenderImage = senderUser.LastName,
                    SenderUserName = senderUser.UserName,
                    ReceiverFirstName = receiverUser.FirstName,
                    ReceiverImage = receiverUser.Image,
                    ReceiverLastName = receiverUser.LastName,
                    ReceiverUserName = receiverUser.UserName
                };
                userMessageDTO.Add(usermessage);
            }
            //return _context.UserMessages.Where(p => (p.SenderApplicationUserId == SenderId && p.ReceiverApplicationUserId == ReceiverId) || (p.SenderApplicationUserId == ReceiverId  && p.ReceiverApplicationUserId == SenderId)).OrderBy(s => s.SendDate).Select(p => new UserMessageDTO
            //{
            //    Id = p.Id,
            //    MessageText = p.MessageText,
            //    SendDate = p.SendDate,
            //});
            return userMessageDTO;
        }
    }
}
