using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;

namespace Message.Data.DAL.Repository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {

    }
    public class ApplicationUserRepository : EfRepository<ApplicationUser> , IApplicationUserRepository
    {
        public ApplicationUserRepository(MessageContext context) : base(context)
        {

        }
    }
}
