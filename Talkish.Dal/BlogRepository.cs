using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
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

        public async Task<List<BlogDTO>> GetAllBlogsAsync()
        {
            List<Blog> blogs = await _ctx.Blogs.Include((b) => b.Author).ToListAsync();
            List<BlogDTO> blogDTOs = new List<BlogDTO>();

            blogs.ForEach((blog) => {
                var blogDTO = new BlogDTO()
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    Content = blog.Content,
                    Author = new BlogAuthorDTO()
                    {
                        AuthorId = blog.Author.AuthorId,
                        FirstName = blog.Author.FirstName,
                        LastName = blog.Author.LastName,
                    },
                };

                blogDTOs.Add(blogDTO);
            });

            return blogDTOs;
        }

        public async Task<BlogDTO> GetBlogByIdAsync(int id)
        {
            Blog blog = await _ctx.Blogs.Where((blog) => blog.BlogId == id).FirstOrDefaultAsync();
            BlogDTO blogDTO = new BlogDTO()
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                Author = new BlogAuthorDTO() { 
                    AuthorId = blog.Author.AuthorId,
                    FirstName = blog.Author.FirstName,
                    LastName = blog.Author.LastName,
                },
            };
            // Blog blog = await _ctx.Blogs.Include((b) => b.Author).FirstOrDefaultAsync((b) => b.BlogId == id);
            return blogDTO;
        }

        public async Task<Blog> UpdateBlogAsync(Blog blogData)
        {
            _ctx.Blogs.Update(blogData);
            await _ctx.SaveChangesAsync();
            return blogData;
        }
    }
}
