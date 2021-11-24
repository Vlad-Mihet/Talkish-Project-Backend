using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Models;

namespace Talkish.API.Interfaces
{
    public interface ITopicService
    {
        Task<Topic> CreateTopic(Topic TopicData);

        Task<List<Topic>> GetAllTopics();

        Task<Topic> GetTopicById(int Id);

        Task<Topic> UpdateTopic(Topic TopicData);

        Task<Topic> DeleteTopicById(int Id);
    }
}
