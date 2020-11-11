using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Message.Data.DAL.Repository
{
    public interface IUserMessageRepository : IRepository<UserMessage>
    {
        IQueryable<UserMessageDTO> ListMessage(Guid SenderId, Guid ReceiverId);
    }
    public class UserMessageRepository : EfRepository<UserMessage> , IUserMessageRepository
    {
        public UserMessageRepository(MessageContext context) : base(context)
        {

        }
        public IQueryable<UserMessageDTO> ListMessage(Guid SenderId, Guid ReceiverId)
        {
            return _context.UserMessages.Where(p => (p.SenderApplicationUserId == SenderId && p.ReceiverApplicationUserId == ReceiverId) || (p.SenderApplicationUserId == ReceiverId  && p.ReceiverApplicationUserId == SenderId)).OrderBy(s => s.SendDate).Select(p => new UserMessageDTO
            {
                Id = p.Id,
                MessageText = p.MessageText,
                SendDate = p.SendDate,
            });
        }
    }
}
