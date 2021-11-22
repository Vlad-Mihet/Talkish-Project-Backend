using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Models;

namespace Talkish.API.Interfaces
{
    public interface ITopicRepository
    {
        Task<TopicDTO> GetTopicByIdAsync(int id);

        Task<List<TopicDTO>> GetAllTopicsAsync();

        Task<TopicDTO> CreateTopicAsync(Topic topic);

        Task<TopicDTO> UpdateTopicAsync(Topic topic);

        Task<TopicDTO> DeleteTopicByIdAsync(int id);
    }
}
