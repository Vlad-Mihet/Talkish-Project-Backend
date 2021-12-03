using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IPublicationRepository
    {
        Task<Publication> GetPublicationByIdAsync(int Id);

        Task<Publication> GetPublicationWithBlogsByIdAsync(int Id);

        Task<List<Publication>> GetAllPublicationsAsync();

        Task<List<Blog>> GetAllPublicationBlogsAsync(int Id);

        Task<List<Author>> GetAllPublicationAuthorsAsync(int Id);

        Task<Publication> CreatePublicationAsync(Publication PublicationData);

        Task<Publication> UpdatePublicationAsync(int PublicationId, Publication PublicationData);

        Task<Publication> AddAuthorToPublicationAsync(int PublicationId, int AuthorId);

        Task<Publication> AddBlogToPublicationAsync(int PublicationId, int BlogId);

        Task<Publication> DeletePublicationByIdAsync(int Id);
    }
}
