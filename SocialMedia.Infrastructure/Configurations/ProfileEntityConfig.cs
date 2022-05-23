using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations
{
    public class ProfileEntityConfig : IEntityTypeConfiguration<ProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne(x => x.Profile)
                .HasForeignKey<ProfileEntity>(x => x.Id);
        }
    }
}
