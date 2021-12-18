using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
                Author createdAuthor = await _service.CreateAuthor(author);


                SuccessResponse response = new()
                {
                    Payload = AuthorData,
                    Status = 201
                };

                return CreatedAtAction(nameof(GetAuthorById), new { Id = createdAuthor.AuthorId }, response);
            } else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();

                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid Author Data",
                    Errors = new List<string>(errors),
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
            if (author == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Author not found",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }

            AuthorWithBlogsDTO authorDTO = _mapper.Map<AuthorWithBlogsDTO>(author);

            SuccessResponse response = new()
            {
                Payload = authorDTO,
                Status = 200
            };

            return Ok(response);
        }

        [Route("{Id}/Blogs")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorBlogsByAuthorIdAsync([FromRoute] int Id)
        {
            List<Blog> blogs = await _service.GetAuthorBlogs(Id);
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);

            if (blogs == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't find author or his blogs, please try again later",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            SuccessResponse response = new()
            {
                Payload = blogDTOs,
                Status = 200
            };

            return StatusCode(200, response);
        }

        [HttpPatch]
        [Route("{AuthorId}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] int AuthorId, [FromBody] UpdateAuthorDTO AuthorData)
        {
            if (ModelState.IsValid)
            {
                Author author = _mapper.Map<Author>(AuthorData);
                await _service.UpdateAuthor(AuthorId, author);


                SuccessResponse response = new()
                {
                    Payload = AuthorData,
                    Status = 200
                };

                return Ok(response);
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();

                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid Author Data",
                    Errors = new List<string>(errors),
                    Status = 400,
                };

                return BadRequest(error);
            }
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int Id)
        {
            Author author = await _service.DeleteAuthorById(Id);

            if (author == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "The author you are currently trying to remove doesn't exist",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            AuthorDTO authorDTO = _mapper.Map<AuthorDTO>(author);

            SuccessResponse response = new()
            {
                Payload = authorDTO,
                Status = 200
            };

            return Ok(response);
        }
    }
}
