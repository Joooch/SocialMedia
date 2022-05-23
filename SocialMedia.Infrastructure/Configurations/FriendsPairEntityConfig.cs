using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations
{
    public class FriendsPairEntityConfig : IEntityTypeConfiguration<FriendsPairEntity>
    {
        public void Configure(EntityTypeBuilder<FriendsPairEntity> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Friend)
                .WithMany()
                .HasForeignKey(p => p.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
