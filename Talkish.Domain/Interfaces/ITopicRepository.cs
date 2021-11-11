using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
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
