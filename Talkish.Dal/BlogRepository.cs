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
            var blogToRemove = new Blog() { BlogId = id };
            _ctx.Blogs.Attach(blogToRemove);
            _ctx.Blogs.Remove(blogToRemove);
            await _ctx.SaveChangesAsync();
            return blogToRemove;
        }

        public async Task<List<BlogWithAuthor>> GetAllBlogsAsync()
        {
            return await _ctx
                .Blogs
                .Join(_ctx.Authors,
                    blog => blog.AuthorId,
                    author => author.AuthorId,
                    (blog, author) => new
                    BlogWithAuthor() {
                        BlogId = blog.BlogId,
                        Title = blog.Title,
                        Content = blog.Content,
                        AuthorId = author.AuthorId,
                        AuthorName = $"{author.FirstName} {author.LastName}",
                    }
                )
                .ToListAsync();
        }

        public async Task<List<Blog>> GetAuthorsBlogsByAuthorIdAsync(int authorId)
        {
            List<Blog> authorBlogs = await _ctx.Blogs.Where(blog => blog.AuthorId == authorId).ToListAsync();
            return authorBlogs;
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            Blog blog = await _ctx.Blogs.FindAsync(id);
            return blog;
        }

        public async Task<Blog> UpdateBlogAsync(Blog blogData)
        {
            await Task.Run(() => _ctx.Blogs.Update(blogData));
            await _ctx.SaveChangesAsync();
            return blogData;
        }
    }
}
