using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> GetBlogByIdAsync(int Id);

        Task<List<Blog>> GetAllBlogsAsync();

        Task<List<Blog>> GetAuthorsBlogsByAuthorIdAsync(int authorId);

        Task<Blog> UpdateBlogAsync(Blog blogData);

        Task<Blog> DeleteBlogByIdAsync(int Id);

        Task<Blog> CreateBlogAsync(Blog blog);
    }
}
