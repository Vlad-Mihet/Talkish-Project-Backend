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
        }
    }
}
