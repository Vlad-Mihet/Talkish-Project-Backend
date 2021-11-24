using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Interfaces;
using Talkish.API.Models;

namespace Talkish.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _repo;

        public TopicService(ITopicRepository repo)
        {
            _repo = repo;
        }

        public async Task<Topic> CreateTopic(Topic TopicData)
        {
            Topic topic = await _repo.CreateTopicAsync(TopicData);
            return topic;
        }
        

        public async Task<List<Topic>> GetAllTopics()
        {
            List<Topic> topics = await _repo.GetAllTopicsAsync();
            return topics;
        }

        public async Task<Topic> GetTopicById(int Id)
        {
            Topic topic = await _repo.GetTopicByIdAsync(Id);
            return topic;
        }

        public async Task<Topic> UpdateTopic(Topic TopicData)
        {
            Topic topic = await _repo.UpdateTopicAsync(TopicData);
            return topic;
        }

        public async Task<Topic> DeleteTopicById(int Id)
        {
            Topic topic = await _repo.DeleteTopicByIdAsync(Id);
            return topic;
        }
    }
}
