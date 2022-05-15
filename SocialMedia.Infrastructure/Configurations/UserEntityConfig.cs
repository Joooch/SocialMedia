using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Configurations
{
    public class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.Email)
                .IsUnicode(true);
        }
    }
}
