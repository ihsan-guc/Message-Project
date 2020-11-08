using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Message.Data.DAL.Repository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        IQueryable<ApplicationUser> SearchApplicationUserList(string searchText);
         
    }
    public class ApplicationUserRepository : EfRepository<ApplicationUser> , IApplicationUserRepository
    {
        public ApplicationUserRepository(MessageContext context) : base(context)
        {

        }
        public IQueryable<ApplicationUser> SearchApplicationUserList(string searchText)
        {
            return _context.ApplicationUsers.Where(p=>p.Email.Contains(searchText) || p.UserName.Contains(searchText));
        }
    }
}
