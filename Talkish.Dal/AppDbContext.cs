using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talkish.Domain.Models;

namespace Talkish.Dal
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
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

        public DbSet<Follower> Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
             base.OnModelCreating(builder);

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

            builder.Entity<User>()
                .HasOne((user) => user.AuthorProfile)
                .WithOne((authorProfile) => authorProfile.UserProfile)
                .HasForeignKey<User>((user) => user.AuthorId);

            builder.Entity<Author>()
                .HasOne((author) => author.UserProfile)
                .WithOne((user) => user.AuthorProfile)
                .HasForeignKey<Author>((author) => author.UserId);

        }
    }
}
