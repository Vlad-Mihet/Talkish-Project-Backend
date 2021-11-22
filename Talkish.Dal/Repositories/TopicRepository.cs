using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.API.DTOs;
using Talkish.API.Interfaces;
using Talkish.API.Models;

namespace Talkish.Dal.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;
        public TopicRepository(AppDbContext ctx, IMapper mapper) {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<TopicDTO> CreateTopicAsync(Topic topic)
        {
            await _ctx.Topics.AddAsync(topic);
            await _ctx.SaveChangesAsync();
            TopicDTO topicDTO = _mapper.Map<TopicDTO>(topic);
            return topicDTO;
        }

        public async Task<TopicDTO> DeleteTopicByIdAsync(int id)
        {
            Topic topicToDelete = await _ctx.Topics.FirstOrDefaultAsync(topic => topic.TopicId == id);
            _ctx.Remove(topicToDelete);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<TopicDTO>(topicToDelete);
        }

        public async Task<List<TopicDTO>> GetAllTopicsAsync()
        {
            List<Topic> topics = await _ctx.Topics
                .Include((topic) => topic.Blogs)
                .ToListAsync();
            List<TopicDTO> topicDTOs = _mapper.Map<List<TopicDTO>>(topics);
            return topicDTOs;
        }

        public async Task<TopicDTO> GetTopicByIdAsync(int id)
        {
            Topic topic = await _ctx.Topics.FindAsync(id);
            return _mapper.Map<TopicDTO>(topic);
        }

        public async Task<TopicDTO> UpdateTopicAsync(Topic topic)
        {
            _ctx.Topics.Update(topic);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<TopicDTO>(topic);
        }
    }
}
