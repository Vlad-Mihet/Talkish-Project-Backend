﻿using AutoMapper;
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

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTopicById(int Id)
        {
            Topic topic = await _service.GetTopicById(Id);
            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTopic(Topic TopicData)
        {
            Topic topic = await _service.UpdateTopic(TopicData);
            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTopic(int Id)
        {
            Topic topic = await _service.DeleteTopicById(Id);
            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }
    }
}
