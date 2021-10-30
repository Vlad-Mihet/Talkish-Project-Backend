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
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogs;

        public BlogController(IBlogRepository blogs)
        {
            _blogs = blogs;
        }

        /* TODO: */
        // Add Error Handling

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBlogById(int id)
        {
            return Ok(await _blogs.GetBlogByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            return Ok(await _blogs.GetAllBlogsAsync());
        }

        [Route("{authorId}/blogs")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorsBlogs(int authorId)
        {
            return Ok(await _blogs.GetAuthorsBlogsByAuthorIdAsync(authorId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] Blog blog)
        {
            return Ok(await _blogs.CreateBlogAsync(blog));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBlogById([FromBody] Blog blog)
        {
            return Ok(await _blogs.UpdateBlogAsync(blog));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBlogById(int id)
        {
            return Ok(await _blogs.DeleteBlogByIdAsync(id));
        }
    }
}
