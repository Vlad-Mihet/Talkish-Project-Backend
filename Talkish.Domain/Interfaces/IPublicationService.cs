using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IPublicationService
    {
        Task<Publication> CreatePublication(Publication PublicationData);

        Task<List<Publication>> GetAllPublications();

        Task<Publication> GetPublicationById(int Id);

        Task<List<Author>> GetPublicationAuthors(int Id);

        Task<List<Blog>> GetPublicationBlogs(int Id);

        Task<Publication> UpdatePublication(Publication PublicationData);

        Task<Publication> DeletePublication(int Id);
    }
}
