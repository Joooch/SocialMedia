using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations
{
    public class FriendsRequestEntityConfig : IEntityTypeConfiguration<FriendsRequestEntity>
    {
        public void Configure(EntityTypeBuilder<FriendsRequestEntity> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Target)
                .WithMany()
                .HasForeignKey(p => p.TargetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
