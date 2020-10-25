using Message.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Message.Data.DAL.Mapping
{
    public class ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMapping()
        {

        }
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(navigationExpression: m => m.Token)
                .WithOne(navigationExpression: g => g.ApplicationUser)
                .HasForeignKey<Token>(s => s.ApplicationUserId);
        }
    }
}
