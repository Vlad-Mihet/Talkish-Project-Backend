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
            List<Blog> blogs = await _ctx.Blogs.Where((b) => b.AuthorId == id).Include((b) => b.Author).ToListAsync();
            return blogs;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _ctx.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            Author author = await _ctx.Authors.FindAsync(id);
            return author;
        }

        public async Task<Author> UpdateAuthorAsync(Author authorData)
        {
            await Task.Run(() => _ctx.Authors.Update(authorData));
            await _ctx.SaveChangesAsync();
            return authorData;
        }
    }
}
