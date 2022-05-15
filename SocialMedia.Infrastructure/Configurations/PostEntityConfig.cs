using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Configurations
{
    public class PostEntityConfig : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.HasKey(c => c.PostId);
        }
    }
}
