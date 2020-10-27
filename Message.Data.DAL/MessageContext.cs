using Message.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Message.Data.DAL
{
    public class MessageContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Token> Tokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1;port=3306;user=root;password=12345678;database=MessageDb")
                .EnableSensitiveDataLogging().EnableDetailedErrors();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>()
                .HasOne(b => b.ApplicationUser)
                .WithOne(i => i.Token)
                .HasForeignKey<Token>(b => b.ApplicationUserId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
