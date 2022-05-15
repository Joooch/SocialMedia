using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Configurations
{
    public class LikePairEntityConfig : IEntityTypeConfiguration<LikePairEntity>
    {
        public void Configure(EntityTypeBuilder<LikePairEntity> builder)
        {
            builder.HasKey(x => x.PostId);

            builder.HasOne(x => x.Post)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
