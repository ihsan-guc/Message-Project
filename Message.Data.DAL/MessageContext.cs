using Message.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Message.Data.DAL
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options):base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
