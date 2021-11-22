using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.DTOs;
using Talkish.API.Interfaces;
using Talkish.API.Models;

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

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBlogById(int id)
        {
            Blog blog = await _service.GetBlogById(id);
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

        [Route("{id}/topics")]
        [HttpGet]
        public async Task<IActionResult> GetBlogTopicsByBlogId(int Id)
        {
            List<Topic> topics = await _service.GetBlogTopicsById(Id);
            List<TopicDTO> topicDTOs = _mapper.Map<List<TopicDTO>>(topics);
            return Ok(topicDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] Blog BlogData)
        {
            Blog blog = await _service.CreateBlog(BlogData);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return Ok(blogDTO);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBlogById([FromBody] Blog BlogData)
        {
            Blog blog = await _service.UpdateBlog(BlogData);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return Ok(blogDTO);
        }

        [Route("{id}/add-topic/{topicId}")]
        [HttpPut]
        public async Task<IActionResult> AddTopicToBlog([FromRoute] int BlogId, [FromRoute] int TopicId)
        {
            Blog blog = await _service.AddTopicToBlog(BlogId, TopicId);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return Ok(blogDTO);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBlogById(int id)
        {
            Blog blog = await _service.DeleteBlogById(id);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return Ok(blogDTO);
        }
    }
}
