using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public BlogRepository(AppDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
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
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);

            return blogDTOs;
        }

        public async Task<BlogDTO> GetBlogByIdAsync(int id)
        {
            Blog blog = await _ctx.Blogs
                .Where((blog) => blog.BlogId == id)
                .Include((b) => b.Author)
                .FirstOrDefaultAsync();
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
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
