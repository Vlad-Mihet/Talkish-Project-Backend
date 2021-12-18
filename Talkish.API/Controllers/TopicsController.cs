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
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _service;
        private readonly IMapper _mapper;

        public TopicsController(ITopicService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] AddTopicDTO TopicData)
        {
            if (ModelState.IsValid)
            {
                Topic topic = _mapper.Map<Topic>(TopicData);
                Topic createdTopic = await _service.CreateTopic(topic);

                SuccessResponse response = new()
                {
                    Payload = TopicData,
                    Status = 200,
                };
                
                return CreatedAtAction(nameof(GetTopicById), new { Id = createdTopic.TopicId }, response);
            }

            List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(t => t.ErrorMessage)).ToList();

            ErrorResponse error = new()
            {
                ErrorMessage = "Invalid Topic Data",
                Errors = errors,
                Status = 400,
            };

            return BadRequest(error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            List<Topic> topics = await _service.GetAllTopics();
            List<TopicDTO> topicDTOs = _mapper.Map<List<TopicDTO>>(topics);

            SuccessResponse response = new()
            {
                Payload = topicDTOs,
                Status = 200,
            };

            return Ok(response);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetTopicById([FromRoute] int Id)
        {
            Topic topic = await _service.GetTopicById(Id);

            if (topic == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Topic not found",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }

            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);

            SuccessResponse response = new()
            {
                Payload = topicDTO,
                Status = 200,
            };

            return Ok(response);
        }

        [HttpPatch]
        [Route("{TopicId}")]
        public async Task<IActionResult> UpdateTopic([FromRoute] int TopicId, [FromBody] UpdateTopicDTO TopicData)
        {
            if (ModelState.IsValid)
            {
                Topic topic = _mapper.Map<Topic>(TopicData);
                await _service.UpdateTopic(TopicId, topic);

                SuccessResponse response = new()
                {
                    Payload = topic,
                    Status = 200,
                };
                
                return Ok(response);
            }

            List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(t => t.ErrorMessage)).ToList();

            ErrorResponse error = new()
            {
                ErrorMessage = "Invalid Topic Data",
                Errors = errors,
                Status = 400,
            };

            return BadRequest(error);
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTopic([FromRoute] int Id)
        {
            Topic topic = await _service.DeleteTopicById(Id);

            if (topic == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "There was an issue removing the topic",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);

            SuccessResponse response = new()
            {
                Payload = topicDTO,
                Status = 200,
            };

            return Ok(response);
        }
    }
}
