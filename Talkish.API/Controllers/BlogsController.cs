using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.DTOs;
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
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return Ok(blogDTO);
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
            return Ok(BlogData);
        }

        [HttpPatch]
        [Route("{BlogId}")]
        public async Task<IActionResult> UpdateBlog([FromRoute] int BlogId, [FromBody] UpdateBlogDTO BlogData)
        {
            Blog blog = _mapper.Map<Blog>(BlogData);
            await _service.UpdateBlog(BlogId, blog);
            return Ok(blog);
        }

        [Route("{BlogId}/add-topic/{TopicId}")]
        [HttpPut]
        public async Task<IActionResult> AddTopicToBlog([FromRoute] int BlogId, [FromRoute] int TopicId)
        {
            Blog blog = await _service.AddTopicToBlog(BlogId, TopicId);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return Ok(blogDTO);
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBlogById([FromRoute] int Id)
        {
            Blog blog = await _service.DeleteBlogById(Id);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return Ok(blogDTO);
        }
    }
}
