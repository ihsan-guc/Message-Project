using System;

namespace Message.Data.DAL.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        public MessageContext context;
        public UnitOfWork(MessageContext messageContext, IApplicationUserRepository applicationUserRepository,ITokenRepository tokenRepository,
            IUserMessageRepository userMessageRepository)
        {
            context = messageContext;
            ApplicationUserRepository = applicationUserRepository;
            UserMessageRepository = userMessageRepository;
            TokenRepository = tokenRepository;
        }
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        public ITokenRepository TokenRepository{ get; set; }
        public IUserMessageRepository UserMessageRepository { get; set; }

        public int Commit()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
