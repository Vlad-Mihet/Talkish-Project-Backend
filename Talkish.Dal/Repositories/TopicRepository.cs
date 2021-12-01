using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _ctx;
        public TopicRepository(AppDbContext ctx) {
            _ctx = ctx;
        }

        public async Task<Topic> CreateTopicAsync(Topic TopicData)
        {
            _ctx.Topics.Add(TopicData);
            await _ctx.SaveChangesAsync();
            return TopicData;
        }

        public async Task<Topic> DeleteTopicByIdAsync(int Id)
        {
            Topic topicToDelete = await _ctx.Topics.FirstOrDefaultAsync(topic => topic.TopicId == Id);
            _ctx.Remove(topicToDelete);
            await _ctx.SaveChangesAsync();
            return topicToDelete;
        }

        public async Task<List<Topic>> GetAllTopicsAsync()
        {
            List<Topic> topics = await _ctx.Topics
                .Include((topic) => topic.Blogs)
                .ToListAsync();
            return topics;
        }

        public async Task<Topic> GetTopicByIdAsync(int Id)
        {
            Topic topic = await _ctx.Topics.FindAsync(Id);
            return topic;
        }

        public async Task<Topic> UpdateTopicAsync(Topic TopicData)
        {
            _ctx.Topics.Update(TopicData);
            await _ctx.SaveChangesAsync();
            return TopicData;
        }
    }
}
