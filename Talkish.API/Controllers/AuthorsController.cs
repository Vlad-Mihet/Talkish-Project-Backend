using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.DTOs;
using Talkish.API.Responses;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.API.Controllers
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

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AddAuthorDTO AuthorData)
        {
            if (ModelState.IsValid)
            {
                Author author = _mapper.Map<Author>(AuthorData);
                await _service.CreateAuthor(author);


                SuccessResponse response = new()
                {
                    Payload = AuthorData,
                    Status = 201
                };

                return CreatedAtAction(nameof(GetAuthorById), response);
            } else
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid Author Data",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            List<Author> authors = await _service.GetAllAuthors();
            List<AuthorWithBlogsDTO> authorDTOs = _mapper.Map<List<AuthorWithBlogsDTO>>(authors);
            return Ok(authorDTOs);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorById([FromRoute] int Id)
        {
            Author author = await _service.GetAuthorById(Id);
            AuthorWithBlogsDTO authorDTO = _mapper.Map<AuthorWithBlogsDTO>(author);
            return Ok(authorDTO);
        }

        [Route("{Id}/Blogs")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorBlogsByAuthorIdAsync([FromRoute] int Id)
        {
            List<Blog> blogs = await _service.GetAuthorBlogs(Id);
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            return Ok(blogDTOs);
        }

        [HttpPatch]
        [Route("{AuthorId}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] int AuthorId, [FromBody] UpdateAuthorDTO AuthorData)
        {
            Author author = _mapper.Map<Author>(AuthorData);
            await _service.UpdateAuthor(AuthorId, author);
            return Ok(AuthorData);
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int Id)
        {
            Author author = await _service.DeleteAuthorById(Id);
            AuthorWithBlogsDTO authorDTO = _mapper.Map<AuthorWithBlogsDTO>(author);
            return Ok(authorDTO);
        }
    }
}
