using System;

namespace Message.Data.DAL.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        public MessageContext context;
        public UnitOfWork(MessageContext messageContext, IApplicationUserRepository applicationUserRepository,ITokenRepository tokenRepository,
            IUserMessageRepository userMessageRepository, IApplicationUserEmailRepository applicationUserEmailRepository,
            IApplicationAdminRepository applicationAdminRepository)
        {
            context = messageContext;
            ApplicationUserEmailRepository = applicationUserEmailRepository;
            ApplicationAdminRepository = applicationAdminRepository;
            ApplicationUserRepository = applicationUserRepository;
            UserMessageRepository = userMessageRepository;
            TokenRepository = tokenRepository;
        }
        public IApplicationUserEmailRepository ApplicationUserEmailRepository{ get; set; }
        public IApplicationAdminRepository  ApplicationAdminRepository{ get; set; }
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        public IUserMessageRepository UserMessageRepository { get; set; }
        public ITokenRepository TokenRepository{ get; set; }

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
