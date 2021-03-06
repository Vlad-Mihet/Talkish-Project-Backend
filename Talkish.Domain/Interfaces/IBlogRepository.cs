using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> GetBlogByIdAsync(int Id);

        Task<List<Blog>> GetAllBlogsAsync();

        Task<Blog> UpdateBlogAsync(int BlogId, Blog blogData);

        Task<Blog> AddTopicToBlogAsync(int Id, int TopicId);

        Task<List<Topic>> GetBlogTopicsByBlogIdAsync(int Id);

        Task<Blog> DeleteBlogByIdAsync(int Id);

        Task<Blog> CreateBlogAsync(Blog blog);
    }
}
