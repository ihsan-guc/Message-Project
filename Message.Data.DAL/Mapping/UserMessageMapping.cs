using Message.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Message.Data.DAL.Mapping
{
    public class UserMessageMapping : IEntityTypeConfiguration<UserMessage>
    {
        public UserMessageMapping()
        {

        }
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.HasOne(navigationExpression: m => m.SenderUser)
                .WithMany(navigationExpression: g => g.SendingMessages)
                .HasForeignKey(s => s.SenderApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(navigationExpression: m => m.ReceiverUser)
                .WithMany(navigationExpression: g => g.ReceivingMessages)
                .HasForeignKey(s => s.ReceiverApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
