using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations
{
    public class ChatMessagesEntityConfig : IEntityTypeConfiguration<ChatMessageEntity>
    {
        public void Configure(EntityTypeBuilder<ChatMessageEntity> builder)
        {
            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Target)
                .WithMany()
                .HasForeignKey(p => p.TargetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
