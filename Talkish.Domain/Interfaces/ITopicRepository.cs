using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Models;

namespace Talkish.API.Interfaces
{
    public interface ITopicRepository
    {
        Task<Topic> GetTopicByIdAsync(int id);

        Task<List<Topic>> GetAllTopicsAsync();

        Task<Topic> CreateTopicAsync(Topic topic);

        Task<Topic> UpdateTopicAsync(Topic topic);

        Task<Topic> DeleteTopicByIdAsync(int id);
    }
}
