using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Models;

namespace Talkish.API.Interfaces
{
    public interface ITopicRepository
    {
        Task<Topic> GetTopicByIdAsync(int Id);

        Task<List<Topic>> GetAllTopicsAsync();

        Task<Topic> CreateTopicAsync(Topic TopicData);

        Task<Topic> UpdateTopicAsync(Topic TopicData);

        Task<Topic> DeleteTopicByIdAsync(int Id);
    }
}
