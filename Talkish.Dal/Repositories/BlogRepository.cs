using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _ctx;

        public BlogRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            _ctx.Blogs.Add(blog);
            await _ctx.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> DeleteBlogByIdAsync(int Id)
        {
            Blog blogToRemove = await _ctx.Blogs.FirstOrDefaultAsync((b) => b.AuthorId == Id);
            _ctx.Blogs.Remove(blogToRemove);
            await _ctx.SaveChangesAsync();
            return blogToRemove;
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            List<Blog> blogs = await _ctx.Blogs
                .Include((b) => b.Author)
                .Include((b) => b.Topics)
                .ToListAsync();

            return blogs;
        }

        public async Task<Blog> GetBlogByIdAsync(int Id)
        {
            Blog blog = await _ctx.Blogs
                .Where((blog) => blog.BlogId == Id)
                .Include((b) => b.Author)
                .Include((b) => b.Topics)
                .FirstOrDefaultAsync();
            return blog;
        }

        public async Task<List<Topic>> GetBlogTopicsByBlogIdAsync(int Id)
        {
            Blog blog = await _ctx.Blogs
                .Include((blog) => blog.Topics)
                .FirstOrDefaultAsync((blog) => blog.BlogId == Id);
            return blog.Topics;
        }

        public async Task<Blog> UpdateBlogAsync(int BlogId, Blog BlogData)
        {
            Blog blog = await _ctx.Blogs.FirstOrDefaultAsync((blog) => blog.BlogId == BlogId);
            blog.Title = BlogData.Title;
            blog.Content = BlogData.Content;
            _ctx.Blogs.Update(blog);
            await _ctx.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> AddTopicToBlogAsync(int Id, int TopicId)
        {
            Blog blog = await _ctx.Blogs
                .Include((blog) => blog.Topics)
                .Include((blog) => blog.Author)
                .FirstOrDefaultAsync((blog) => blog.BlogId == Id);
            Topic topic = await _ctx.Topics
                .Where((topic) => topic.TopicId == TopicId)
                .FirstOrDefaultAsync();

            blog.Topics.Add(topic);
            await _ctx.SaveChangesAsync();
            
            return blog;
        }
    }
}
