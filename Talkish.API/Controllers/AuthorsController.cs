using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /* TODO: */
        // Add Error Handling

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author AuthorData)
        {
            Author author = await _service.CreateAuthor(AuthorData);
            BlogAuthorDTO authorDTO = _mapper.Map<BlogAuthorDTO>(author);
            return Ok(authorDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            List<Author> authors = await _service.GetAllAuthors();
            List<BlogAuthorDTO> authorDTOs = _mapper.Map<List<BlogAuthorDTO>>(authors);
            return Ok(authorDTOs);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorById(int Id)
        {
            Author author = await _service.GetAuthorById(Id);
            BlogAuthorDTO authorDTO = _mapper.Map<BlogAuthorDTO>(author);
            return Ok(authorDTO);
        }

        [Route("{id}/Blogs")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorBlogsByAuthorIdAsync(int Id)
        {
            List<Blog> blogs = await _service.GetAuthorBlogs(Id);
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            return Ok(blogDTOs);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(Author AuthorData)
        {
            Author author = await _service.UpdateAuthor(AuthorData);
            BlogAuthorDTO authorDTO = _mapper.Map<BlogAuthorDTO>(author);
            return Ok(authorDTO);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {
            Author author = await _service.DeleteAuthorById(Id);
            BlogAuthorDTO authorDTO = _mapper.Map<BlogAuthorDTO>(author);
            return Ok(authorDTO);
        }
    }
}
