using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authors;

        public AuthorsController(IAuthorRepository authors)
        {
            _authors = authors;
        }

        /* TODO: */
        // Add Error Handling

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            return Ok(await _authors.GetAuthorByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _authors.GetAllAuthorsAsync());
        }

        [Route("{id}/Blogs")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorBlogsByAuthorIdAsync(int id)
        {
            return Ok(await _authors.GetAuthorBlogsByAuthorIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            return Ok(await _authors.CreateAuthorAsync(author));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(Author author)
        {
            return Ok(await _authors.UpdateAuthorAsync(author));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return Ok(await _authors.DeleteAuthorByIdAsync(id));
        }
    }
}
