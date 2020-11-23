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
        List<ApplicationUser> ApplicationMessageList(Guid ApplicationUserId);
    }
    public class UserMessageRepository : EfRepository<UserMessage>, IUserMessageRepository
    {
        public UserMessageRepository(MessageContext context) : base(context)
        {

        }

        public List<ApplicationUser> ApplicationMessageList(Guid ApplicationUserId)
        {
            var ReceiverApplicationUserIdList = _context.UserMessages.Where(p => p.SenderApplicationUserId == ApplicationUserId).Select(p=>p.ReceiverApplicationUserId).Distinct().ToList();
            var applicationUserList = new List<ApplicationUser>();
            foreach (var applicationUserId in ReceiverApplicationUserIdList)
            {
                applicationUserList.Add(_context.ApplicationUsers.Where(p => p.Id == applicationUserId).FirstOrDefault());
            }
            return applicationUserList;
        }

        public List<UserMessageDTO> ListMessage(Guid SenderId, Guid ReceiverId)
        {
            var userMessageDTO = new List<UserMessageDTO>();
            var userMessagelist = _context.UserMessages.Where(p => (p.SenderApplicationUserId == SenderId && p.ReceiverApplicationUserId == ReceiverId) || (p.SenderApplicationUserId == ReceiverId && p.ReceiverApplicationUserId == SenderId)).OrderByDescending(s => s.SendDate);
            var senderUser = _context.ApplicationUsers.Where(p => p.Id == SenderId).FirstOrDefault();
            var receiverUser = _context.ApplicationUsers.Where(c => c.Id == ReceiverId).FirstOrDefault();
            string k = receiverUser.UserName;
            foreach (var message in userMessagelist)
            {
                var usermessage = new UserMessageDTO()
                {
                    Id = message.Id,
                    MessageText = message.MessageText,
                    MessageSender = message.SenderApplicationUserId,
                    SendDate = message.SendDate,
                    SenderId = senderUser.Id,
                    SenderFirstName = senderUser.FirstName,
                    SenderLastName = senderUser.LastName,
                    SenderImage = senderUser.Image,
                    SenderUserName = senderUser.UserName,
                    ReceiverId = receiverUser.Id,
                    ReceiverUserName = receiverUser.UserName,
                    ReceiverFirstName = receiverUser.FirstName,
                    ReceiverLastName = receiverUser.LastName,
                    ReceiverImage = receiverUser.Image,
                };
                userMessageDTO.Add(usermessage);
            }
            return userMessageDTO;
        }
    }
}
