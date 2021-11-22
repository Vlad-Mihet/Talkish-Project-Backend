using Microsoft.EntityFrameworkCore;
using Talkish.API.Models;

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
    }
}
