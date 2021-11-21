using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        private int getBlogReadingTime(string BlogContent)
        {
            int blogContentLength = BlogContent.Split(null).Length;
            const int wordsPerMinute = 225;
            return (int)Math.Floor((double)(blogContentLength / wordsPerMinute)) + 1;
        }

        public async Task<BlogDTO> CreateBlog(Blog BlogData)
        {
            Blog blog = await _repo.CreateBlogAsync(BlogData);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return blogDTO;
        }

        public async Task<BlogDTO> GetBlogById(int Id) {
            Blog blog = await _repo.GetBlogByIdAsync(Id);
            blog.ReadingTime = getBlogReadingTime(blog.Content);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);

            return blogDTO;
        }

        public async Task<List<BlogDTO>> GetAllBlogs() {
            List<Blog> blogs = await _repo.GetAllBlogsAsync();
            var processedBlogs = new List<Blog>();

            foreach(var blog in blogs)
            {
                blog.ReadingTime = getBlogReadingTime(blog.Content);
                processedBlogs.Add(blog);
            }

            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(processedBlogs);

            return blogDTOs;
        }

        public async Task<List<TopicDTO>> GetBlogTopicsById(int Id)
        {
            List<Topic> topics = await _repo.GetBlogTopicsByBlogIdAsync(Id);
            List<TopicDTO> topicsDTOs = _mapper.Map<List<TopicDTO>>(topics);
            return topicsDTOs;
        }

        public async Task<BlogDTO> UpdateBlog(Blog BlogData)
        {
            Blog blog = await _repo.UpdateBlogAsync(BlogData);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return blogDTO;
        }

        public async Task<BlogDTO> AddTopicToBlog(int BlogId, int TopicId)
        {
            Blog blog = await _repo.AddTopicToBlogAsync(BlogId, TopicId);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return blogDTO;
        }

        public async Task<BlogDTO> DeleteBlogById(int Id)
        {
            Blog blog = await _repo.DeleteBlogByIdAsync(Id);
            BlogDTO blogDTO = _mapper.Map<BlogDTO>(blog);
            return blogDTO;
        }
    }
}
