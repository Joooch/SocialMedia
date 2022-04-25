using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain;

namespace SocialMedia.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }

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
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsUnicode(true);

            //modelBuilder.Entity<User>().HasOne(x => x.Profile).WithOne(x=>x.Owner);
            //modelBuilder.Entity<User>().HasOne(x => x.Profile);
        }
    }
}