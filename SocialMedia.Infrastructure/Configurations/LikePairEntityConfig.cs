using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Configurations
{
    public class LikePairEntityConfig : IEntityTypeConfiguration<LikeEntity>
    {
        public void Configure(EntityTypeBuilder<LikeEntity> builder)
        {
            builder.HasKey(x => x.PostId);

            builder.HasOne(x => x.Post)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
