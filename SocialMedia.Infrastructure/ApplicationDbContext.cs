using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain;

namespace SocialMedia.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<FriendsPairEntity> Friends { get; set; }
        public DbSet<LikePairEntity> Likes { get; set; }
        public DbSet<PostEntity> Posts { get; set; }

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
            modelBuilder.Entity<UserEntity>()
                .Property(x => x.Email)
                .IsUnicode(true);



            modelBuilder.Entity<LikePairEntity>()
                .HasKey(x => x.PostId);

            modelBuilder.Entity<LikePairEntity>()
                .HasOne(x=>x.Post)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostEntity>()
                .HasKey(c => c.PostId);


            modelBuilder.Entity<FriendsPairEntity>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendsPairEntity>()
                .HasOne(p => p.Friend)
                .WithMany()
                .HasForeignKey(p => p.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<User>().HasOne(x => x.Profile).WithOne(x=>x.Owner);
            //modelBuilder.Entity<User>().HasOne(x => x.Profile);
        }
    }
}