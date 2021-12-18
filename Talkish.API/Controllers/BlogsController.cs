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
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _service;
        private readonly IMapper _mapper;

        public BlogsController(IBlogService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetBlogById([FromRoute] int Id)
        {
            Blog blog = await _service.GetBlogById(Id);
            if (blog == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Blog not found",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);

            SuccessResponse response = new()
            {
                Payload = blogDTO,
                Status = 200
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            List<Blog> blogs = await _service.GetAllBlogs();
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);

            SuccessResponse response = new()
            {
                Payload = blogDTOs,
                Status = 200
            };

            return Ok(response);
        }

        [Route("{Id}/topics")]
        [HttpGet]
        public async Task<IActionResult> GetBlogTopicsByBlogId([FromRoute] int Id)
        {
            List<Topic> topics = await _service.GetBlogTopicsById(Id);
            List<TopicDTO> topicDTOs = _mapper.Map<List<TopicDTO>>(topics);

            if (topics == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't find blog, please try again later",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            SuccessResponse response = new()
            {
                Payload = topicDTOs,
                Status = 200
            };

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] AddBlogDTO BlogData)
        {
            if (ModelState.IsValid)
            {
                Blog blog = _mapper.Map<Blog>(BlogData);
                await _service.CreateBlog(blog);


                SuccessResponse response = new()
                {
                    Payload = BlogData,
                    Status = 201
                };

                return CreatedAtAction(nameof(GetBlogById), new { Id = blog.BlogId }, response);
            } else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();

                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid Blog Data",
                    Errors = errors,
                    Status = 400,
                };

                return BadRequest(error);
            }
        }

        [Route("{BlogId}")]
        [HttpPatch]
        public async Task<IActionResult> UpdateBlog([FromRoute] int BlogId, [FromBody] UpdateBlogDTO BlogData)
        {
            if (ModelState.IsValid)
            {
                Blog blog = _mapper.Map<Blog>(BlogData);
                await _service.UpdateBlog(BlogId, blog);


                SuccessResponse response = new()
                {
                    Payload = BlogData,
                    Status = 200
                };

                return Ok(response);
            } else 
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();

                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid Blog Data",
                    Errors = new List<string>(errors),
                    Status = 400,
                };

                return BadRequest(error);
            }
        }

        [Route("{BlogId}/topics/{TopicId}")]
        [HttpPost]
        public async Task<IActionResult> AddTopicToBlog([FromRoute] int BlogId, [FromRoute] int TopicId)
        {
            Blog blog = await _service.AddTopicToBlog(BlogId, TopicId);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);

            if (blog == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't add topic to blog, please try again later",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            SuccessResponse response = new()
            {
                Payload = blogDTO,
                Status = 200
            };

            return Ok(response);
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBlogById([FromRoute] int Id)
        {
            Blog blog = await _service.DeleteBlogById(Id);

            if (blog == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "The blog you are currently trying to remove doesn't exist",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);

            SuccessResponse response = new()
            {
                Payload = blogDTO,
                Status = 200
            };

            return Ok(response);
        }
    }
}
