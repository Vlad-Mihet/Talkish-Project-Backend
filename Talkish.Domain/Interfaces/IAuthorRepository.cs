using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorByIdAsync(int Id);

        Task<Author> CreateAuthorAsync(Author Author);

        Task<Author> DeleteAuthorByIdAsync(int Id);

        Task<List<Blog>> GetAuthorBlogsByAuthorIdAsync(int Id);

        Task<Author> UpdateAuthorAsync(int AuthorId, Author Author);

        Task<List<Author>> GetAllAuthorsAsync();
    }
}
