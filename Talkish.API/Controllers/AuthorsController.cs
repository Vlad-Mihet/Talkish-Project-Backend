using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Talkish.API.DTOs;
using Talkish.API.Responses;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;
using Talkish.Services;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;
        private readonly IMapper _mapper;
        private readonly AuthService _authService;

        public AuthorsController(IAuthorService service, IMapper mapper, AuthService authService)
        {
            _service = service;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] int UserId)
        {
            if (ModelState.IsValid)
            {
                Author createdAuthor = await _service.CreateAuthor(UserId);
                AuthorDTO createdAuthorDTO = _mapper.Map<AuthorDTO>(createdAuthor);


                SuccessResponse response = new()
                {
                    Payload = createdAuthorDTO,
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

            SuccessResponse response = new()
            {
                Payload = authorDTOs,
                Status = 200,
            };

            return Ok(response);
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
        public async Task<IActionResult> GetAuthorBlogsByAuthorIdAsync([FromRoute] int Id, [FromQuery] bool drafts)
        {
            if (drafts)
            {
                try
                {
                    ClaimsPrincipal authUser = HttpContext.User;

                    if (authUser is null) throw new Exception();

                    int authUserId = await _authService.GetAuthenticatedUserIdAsync(authUser);

                    Author authorByUserId = await _service.GetAuthorByUserId(authUserId);

                    if (!await _authService.IsAuthorAsync(authUser) || authorByUserId.AuthorId != Id) throw new Exception();
                }
                catch (Exception) {
                    return new ForbidResult();
                }
            }

            List<Blog> blogs = drafts
                ? await _service.GetAuthorDraftBlogs(Id)
                : await _service.GetAuthorBlogs(Id);

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
