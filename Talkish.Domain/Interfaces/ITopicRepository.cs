using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
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
