using Microsoft.EntityFrameworkCore;

namespace Message.Data.DAL
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
        {

        }
    }
}
