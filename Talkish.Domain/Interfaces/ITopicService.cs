using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
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
