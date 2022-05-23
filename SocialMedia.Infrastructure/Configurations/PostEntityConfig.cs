using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations
{
    public class PostEntityConfig : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.UserOwner)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            /*builder.HasOne(c => c.UserOwner)
                .WithOne()
                .HasForeignKey<PostEntity>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);*/
        }
    }
}
