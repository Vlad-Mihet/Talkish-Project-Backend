using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _repo;
        public PublicationService(IPublicationRepository repo)
        {
            _repo = repo;
        }

        public async Task<Publication> CreatePublication(Publication PublicationData)
        {
            return await _repo.CreatePublicationAsync(PublicationData);
        }

        public async Task<Publication> DeletePublication(int Id)
        {
            return await _repo.DeletePublicationByIdAsync(Id);
        }

        public async Task<List<Publication>> GetAllPublications()
        {
            return await _repo.GetAllPublicationsAsync();
        }

        public async Task<List<Author>> GetPublicationAuthors(int Id)
        {
            return await _repo.GetAllPublicationAuthorsAsync(Id);
        }

        public async Task<List<Blog>> GetPublicationBlogs(int Id)
        {
            return await _repo.GetAllPublicationBlogsAsync(Id);
        }

        public async Task<Publication> GetPublicationById(int Id)
        {
            return await _repo.GetPublicationByIdAsync(Id);
        }

        public async Task<Publication> UpdatePublication(Publication PublicationData)
        {
            return await _repo.UpdatePublicationAsync(PublicationData);
        }

        public async Task<Publication> AddBlogToPublication(int PublicationId, int BlogId)
        {
            return await _repo.AddBlogToPublicationAsync(PublicationId, BlogId);
        }

        public async Task<Publication> AddAuthorToPublication(int PublicationId, int AuthorId)
        {
            return await _repo.AddAuthorToPublicationAsync(PublicationId, AuthorId);
        }
    }
}
