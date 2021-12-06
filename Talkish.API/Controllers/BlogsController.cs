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
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _service;
        private readonly IMapper _mapper;

        public BlogsController(IBlogService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Route("{Id}", Name = "getBlogById")]
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
            return Ok(blogDTOs);
        }

        [Route("{Id}/topics")]
        [HttpGet]
        public async Task<IActionResult> GetBlogTopicsByBlogId([FromRoute] int Id)
        {
            List<Topic> topics = await _service.GetBlogTopicsById(Id);
            List<TopicDTO> topicDTOs = _mapper.Map<List<TopicDTO>>(topics);
            return Ok(topicDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] AddBlogDTO BlogData)
        {
            Blog blog = _mapper.Map<Blog>(BlogData);
            await _service.CreateBlog(blog);

            if (blog == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't create blog, please try again later",
                    Errors = new List<string>(),
                    Status = 409,
                };

                return Conflict(error);
            }

            SuccessResponse response = new()
            {
                Payload = BlogData,
                Status = 201
            };

            return StatusCode(201, response);
        }

        [Route("{BlogId}")]
        [HttpPatch]
        public async Task<IActionResult> UpdateBlog([FromRoute] int BlogId, [FromBody] UpdateBlogDTO BlogData)
        {
            Blog blog = _mapper.Map<Blog>(BlogData);
            await _service.UpdateBlog(BlogId, blog);

            if (blog == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't update blog, please try again later",
                    Errors = new List<string>(),
                    Status = 409,
                };

                return Conflict(error);
            }

            SuccessResponse response = new()
            {
                Payload = BlogData,
                Status = 200
            };

            return Ok(response);
        }

        [Route("{BlogId}/add-topic/{TopicId}")]
        [HttpPut]
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

                return Conflict(error);
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
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);

            if (blog == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't remove blog, please try again later",
                    Errors = new List<string>(),
                    Status = 409,
                };

                return Conflict(error);
            }

            SuccessResponse response = new()
            {
                Payload = blogDTO,
                Status = 200
            };

            return Ok(response);
        }
    }
}
