using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Infrastructure.Configurations;

namespace SocialMedia.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<FriendsPairEntity> Friends { get; set; }
        public DbSet<LikeEntity> Likes { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<FriendsRequestEntity> FriendRequests { get; set; }
        public DbSet<ChatMessageEntity> ChatMessages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var configAssembly = typeof(UserEntityConfig).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);
        }
    }
}