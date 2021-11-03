using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal
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

        public async Task<Blog> DeleteBlogByIdAsync(int id)
        {
            Blog blogToRemove = await _ctx.Blogs.FirstOrDefaultAsync((b) => b.AuthorId == id);
            _ctx.Blogs.Remove(blogToRemove);
            await _ctx.SaveChangesAsync();
            return blogToRemove;
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _ctx.Blogs.Include((b) => b.Author).ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            Blog blog = await _ctx.Blogs.FirstOrDefaultAsync((b) => b.BlogId == id);
            return blog;
        }

        public async Task<Blog> UpdateBlogAsync(Blog blogData)
        {
            _ctx.Blogs.Update(blogData);
            await _ctx.SaveChangesAsync();
            return blogData;
        }
    }
}
