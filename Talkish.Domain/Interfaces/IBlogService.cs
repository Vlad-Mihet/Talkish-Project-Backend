using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IBlogService
    {
        Task<BlogDTO> CreateBlog(Blog BlogData);

        Task<List<BlogDTO>> GetAllBlogs();

        Task<BlogDTO> GetBlogById(int Id);

        Task<List<TopicDTO>> GetBlogTopicsById(int Id);

        Task<BlogDTO> UpdateBlog(Blog BlogData);

        Task<BlogDTO> AddTopicToBlog(int BlogId, int TopicId);

        Task<BlogDTO> DeleteBlogById(int Id);
    }
}
