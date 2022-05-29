using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repo;

        public AuthorService(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Author> CreateAuthor(int UserId)
        {
            Author author = await _repo.CreateAuthorAsync(UserId);
            return author;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            List<Author> authors = await _repo.GetAllAuthorsAsync();
            return authors;
        }

        public async Task<List<Blog>> GetAuthorBlogs(int Id)
        {
            List<Blog> blogs = await _repo.GetAuthorBlogsByAuthorIdAsync(Id);
            return blogs;
        }

        public async Task<Author> GetAuthorById(int Id)
        {
            Author author = await _repo.GetAuthorByIdAsync(Id);
            return author;
        }

        public async Task<Author> DeleteAuthorById(int Id)
        {
            Author author = await _repo.DeleteAuthorByIdAsync(Id);
            return author;
        }
    }
}
