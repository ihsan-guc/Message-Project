using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;

namespace Message.Data.DAL.Repository
{
    public interface IApplicationAdminRepository : IRepository<ApplicationAdmin>
    {
    }
    public class ApplicationAdminRepository : EfRepository<ApplicationAdmin> , IApplicationAdminRepository
    {
        public ApplicationAdminRepository(MessageContext context) : base(context)
        {

        }
    }
}
