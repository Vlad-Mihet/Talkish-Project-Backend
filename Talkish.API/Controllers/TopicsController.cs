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
            Topic topic = _mapper.Map<Topic>(TopicData);
            await _service.CreateTopic(topic);
            return Ok(topic);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            List<Topic> topics = await _service.GetAllTopics();
            List<TopicDTO> topicDTOs = _mapper.Map<List<TopicDTO>>(topics);
            return Ok(topicDTOs);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetTopicById([FromRoute] int Id)
        {
            Topic topic = await _service.GetTopicById(Id);
            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }

        [HttpPatch]
        [Route("{TopicId}")]
        public async Task<IActionResult> UpdateTopic([FromRoute] int TopicId, [FromBody] UpdateTopicDTO TopicData)
        {
            Topic topic = _mapper.Map<Topic>(TopicData);
            await _service.UpdateTopic(TopicId, topic);
            return Ok(TopicData);
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTopic([FromRoute] int Id)
        {
            Topic topic = await _service.DeleteTopicById(Id);
            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }
    }
}
