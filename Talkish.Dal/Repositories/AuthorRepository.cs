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

        public async Task<Author> CreateAuthorAsync(int UserId)
        {
            User user = await _ctx.Users
                .Include((user) => user.BasicInfo)
                .FirstOrDefaultAsync((user) => user.UserId == UserId);

            Author author = new()
            {
                UserId = user.UserId,
                UserProfile = user,
            };

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
                .Include((author) => author.UserProfile)
                .ThenInclude((userProfile) => userProfile.BasicInfo)
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

        public async Task<List<Blog>> GetAuthorDraftBlogsByAuthorIdAsync(int Id)
        {
            Author author = await _ctx.Authors
                .Include((author) => author.UserProfile)
                .ThenInclude((userProfile) => userProfile.BasicInfo)
                .Include((author) => author.Blogs)
                .ThenInclude((blog) => blog.Topics)
                .FirstOrDefaultAsync((author) => author.AuthorId == Id);

            if (author is null) return null;

            List<Blog> blogs = author.Blogs
                .Where((blog) => blog.IsDraft == true)
                .ToList();

            return blogs;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _ctx.Authors
                .Include((author) => author.UserProfile)
                .ThenInclude((userProfile) => userProfile.BasicInfo)
                .Include((author) => author.Blogs)
                .ThenInclude((blog) => blog.Topics)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int Id)
        {
            Author author = await _ctx.Authors
                .Include((author) => author.UserProfile)
                .ThenInclude((userProfile) => userProfile.BasicInfo)
                .Include((author) => author.Blogs)
                .ThenInclude((blog) => blog.Topics)
                .FirstOrDefaultAsync((author) => author.AuthorId == Id);
            return author;
        }
    }
}
