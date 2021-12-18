using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

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
            _ctx.Authors.Add(author);
            await _ctx.SaveChangesAsync();
            return author;
        }

        public async Task<Author> DeleteAuthorByIdAsync(int id)
        {
            Author authorToRemove = await _ctx.Authors.FirstOrDefaultAsync((a) => a.AuthorId == id);
            
            if (authorToRemove == null)
            {
                return null;
            }
            
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

            if (author == null)
            {
                return null;
            }

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

        public async Task<Author> UpdateAuthorAsync(int AuthorId, Author AuthorData)
        {
            Author author = await _ctx.Authors.FirstOrDefaultAsync((author) => author.AuthorId == AuthorId);
            
            if (author == null)
            {
                return null;
            }

            author.FirstName = AuthorData.FirstName;
            author.LastName = AuthorData.LastName;
            author.Email = AuthorData.Email;
            _ctx.Authors.Update(author);
            await _ctx.SaveChangesAsync();
            return author;
        }
    }
}
