using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> CreateBlog(Blog BlogData);

        Task<List<Blog>> GetAllBlogs();

        Task<Blog> GetBlogById(int Id);

        Task<List<Topic>> GetBlogTopicsById(int Id);

        Task<Blog> UpdateBlog(Blog BlogData);

        Task<Blog> AddTopicToBlog(int BlogId, int TopicId);

        Task<Blog> DeleteBlogById(int Id);
    }
}
