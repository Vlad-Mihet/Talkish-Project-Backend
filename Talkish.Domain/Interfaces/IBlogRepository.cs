using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<BlogDTO> GetBlogByIdAsync(int Id);

        Task<List<BlogDTO>> GetAllBlogsAsync();

        Task<Blog> UpdateBlogAsync(Blog blogData);

        Task<Blog> DeleteBlogByIdAsync(int Id);

        Task<Blog> CreateBlogAsync(Blog blog);
    }
}
