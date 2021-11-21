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
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogs;
        private readonly IBlogService _service;

        public BlogsController(IBlogRepository blogs, IBlogService service)
        {
            _blogs = blogs;
            _service = service;
        }

        /* TODO: */
        // Add Error Handling

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBlogById(int id)
        {
            return Ok(await _service.GetBlogById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            return Ok(await _service.GetAllBlogs());
        }

        [Route("{id}/topics")]
        [HttpGet]
        public async Task<IActionResult> GetBlogTopicsByBlogId(int id)
        {
            return Ok(await _service.GetBlogTopicsById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] Blog blog)
        {
            return Ok(await _service.CreateBlog(blog));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBlogById([FromBody] Blog blog)
        {
            return Ok(await _service.UpdateBlog(blog));
        }

        [Route("{id}/add-topic/{topicId}")]
        [HttpPut]
        public async Task<IActionResult> AddTopicToBlog([FromRoute] int id, [FromRoute] int topicId)
        {
            return Ok(await _service.AddTopicToBlog(id, topicId));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBlogById(int id)
        {
            return Ok(await _service.DeleteBlogById(id));
        }
    }
}
