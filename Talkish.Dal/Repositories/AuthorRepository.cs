using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.API.Interfaces;
using Talkish.API.Models;

namespace Talkish.Dal.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _ctx;

        public AuthorRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            await _ctx.Authors.AddAsync(author);
            await _ctx.SaveChangesAsync();
            return author;
        }

        public async Task<Author> DeleteAuthorByIdAsync(int id)
        {
            Author authorToRemove = await _ctx.Authors.FirstOrDefaultAsync((a) => a.AuthorId == id);
            _ctx.Authors.Remove(authorToRemove);
            await _ctx.SaveChangesAsync();
            return authorToRemove;
        }

        public async Task<List<Blog>> GetAuthorBlogsByAuthorIdAsync(int id)
        {
            Author author = await _ctx.Authors
                .Include((author) => author.Blogs)
                .ThenInclude((blog) => blog.Topics)
                .FirstOrDefaultAsync((author) => author.AuthorId == id);
            List<Blog> blogs = author.Blogs
                .ToList();
            return blogs;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _ctx.Authors
                .Include((author) => author.Blogs)
                .ThenInclude((blog) => blog.Topics)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int Id)
        {
            Author author = await _ctx.Authors
                .Include((author) => author.Blogs)
                .ThenInclude((blog) => blog.Topics)
                .FirstOrDefaultAsync((author) => author.AuthorId == Id);
            return author;
        }

        public async Task<Author> UpdateAuthorAsync(Author authorData)
        {
            _ctx.Authors.Update(authorData);
            await _ctx.SaveChangesAsync();
            return authorData;
        }
    }
}
