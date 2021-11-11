using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _ctx;
        public TopicRepository(AppDbContext ctx) {
            _ctx = ctx;
        }

        public async Task<Topic> CreateTopicAsync(Topic topic)
        {
            await _ctx.Topics.AddAsync(topic);
            await _ctx.SaveChangesAsync();
            return topic;
        }

        public async Task<Topic> DeleteTopicByIdAsync(int id)
        {
            Topic topicToDelete = await _ctx.Topics.FirstOrDefaultAsync(topic => topic.TopicId == id);
            _ctx.Remove(topicToDelete);
            await _ctx.SaveChangesAsync();
            return topicToDelete;
        }

        public async Task<List<Topic>> GetAllTopicsAsync()
        {
            return await _ctx.Topics.ToListAsync();
        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            Topic topic = await _ctx.Topics.FindAsync(id);
            return topic;
        }

        public async Task<Topic> UpdateTopicAsync(Topic topic)
        {
            _ctx.Topics.Update(topic);
            await _ctx.SaveChangesAsync();
            return topic;
        }
    }
}
