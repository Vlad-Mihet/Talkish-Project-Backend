﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Models;

namespace Talkish.API.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> CreateAuthor(Author AuthorData);

        Task<List<Author>> GetAllAuthors();

        Task<Author> GetAuthorById(int Id);

        Task<List<Blog>> GetAuthorBlogs(int Id);

        Task<Author> UpdateAuthor(Author AuthorData);

        Task<Author> DeleteAuthorById(int Id);
    }
}
