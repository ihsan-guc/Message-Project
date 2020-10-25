using System;

namespace Message.Data.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public MessageContext context;
        public UnitOfWork(MessageContext messageContext, IApplicationUserRepository applicationUserRepository)
        {
            context = messageContext;
            ApplicationUserRepository = applicationUserRepository;
        }
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
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
