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

        public DbSet<ApplicationAdmin> ApplicationAdmins{ get; set; }
        public DbSet<ApplicationUserEmail> ApplicationUserEmails{ get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserMessage> UserMessages{ get; set; }
        public DbSet<Token> Tokens{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
