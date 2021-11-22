using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.API.Interfaces;
using Talkish.API.Models;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicRepository _topics;

        public TopicsController(ITopicRepository topics)
        {
            _topics = topics;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            return Ok(await _topics.GetAllTopicsAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTopicById(int id)
        {
            return Ok(await _topics.GetTopicByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(Topic topic)
        {
            return Ok(await _topics.CreateTopicAsync(topic));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTopic(Topic topic)
        {
            return Ok(await _topics.UpdateTopicAsync(topic));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            return Ok(await _topics.DeleteTopicByIdAsync(id));
        }
    }
}
