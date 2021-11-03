using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorByIdAsync(int id);

        Task<Author> CreateAuthorAsync(Author author);

        Task<Author> DeleteAuthorByIdAsync(int id);

        Task<List<Blog>> GetAuthorBlogsByAuthorIdAsync(int id);

        Task<Author> UpdateAuthorAsync(Author author);

        Task<List<Author>> GetAllAuthorsAsync();
    }
}
