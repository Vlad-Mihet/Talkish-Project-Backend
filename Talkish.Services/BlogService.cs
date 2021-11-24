using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Interfaces;
using Talkish.API.Models;

namespace Talkish.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;

        public BlogService(IBlogRepository repo) {
            _repo = repo;
        }

        private static int GetBlogReadingTime(string BlogContent)
        {
            int blogContentLength = BlogContent.Split(null).Length;
            const int wordsPerMinute = 225;
            return Convert.ToInt32(Math.Ceiling((double)blogContentLength / wordsPerMinute));
        }

        public async Task<Blog> CreateBlog(Blog BlogData)
        {
            Blog blog = await _repo.CreateBlogAsync(BlogData);
            return blog;
        }

        public async Task<Blog> GetBlogById(int Id) {
            Blog blog = await _repo.GetBlogByIdAsync(Id);
            blog.ReadingTime = GetBlogReadingTime(blog.Content);

            return blog;
        }

        public async Task<List<Blog>> GetAllBlogs() {
            List<Blog> blogs = await _repo.GetAllBlogsAsync();
            var processedBlogs = new List<Blog>();

            foreach(var blog in blogs)
            {
                blog.ReadingTime = GetBlogReadingTime(blog.Content);
                processedBlogs.Add(blog);
            }

            return processedBlogs;
        }

        public async Task<List<Topic>> GetBlogTopicsById(int Id)
        {
            List<Topic> topics = await _repo.GetBlogTopicsByBlogIdAsync(Id);
            return topics;
        }

        public async Task<Blog> UpdateBlog(Blog BlogData)
        {
            Blog blog = await _repo.UpdateBlogAsync(BlogData);
            return blog;
        }

        public async Task<Blog> AddTopicToBlog(int BlogId, int TopicId)
        {
            Blog blog = await _repo.AddTopicToBlogAsync(BlogId, TopicId);
            return blog;
        }

        public async Task<Blog> DeleteBlogById(int Id)
        {
            Blog blog = await _repo.DeleteBlogByIdAsync(Id);
            return blog;
        }
    }
}
