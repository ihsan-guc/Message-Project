using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;

namespace Message.Data.DAL.Repository
{
    public interface IApplicationUserEmailRepository : IRepository<ApplicationUserEmail>
    {
    }
    public class ApplicationUserEmailRepository : EfRepository<ApplicationUserEmail> , IApplicationUserEmailRepository
    {
        public ApplicationUserEmailRepository(MessageContext context) : base(context)
        {

        }
    }
}
