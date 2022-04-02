using Microsoft.EntityFrameworkCore;
using Talkish.Domain.Models;

namespace Talkish.Dal
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; } 

        public DbSet<Author> Authors { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Publication> Publications { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<BasicInfo> BasicInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Topic>()
                .HasIndex((topic) => topic.Name)
                .IsUnique();

            builder.Entity<Blog>()
                .HasOne((blog) => blog.Author)
                .WithMany((author) => author.Blogs)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Publication>()
                .HasIndex((publication) => publication.Name)
                .IsUnique();

            builder.Entity<Author>()
                .HasOne((author) => author.UserProfile)
                .WithOne((userProfile) => userProfile.AuthorProfile)
                .HasForeignKey<User>((user) => user.UserId);

            builder.Entity<User>()
                .HasMany((user) => user.Followers)
                .WithMany((follower) => follower.Following);
        }
    }
}
