using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly AppDbContext _ctx;

        public PublicationRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Publication> CreatePublicationAsync(Publication PublicationData)
        {
            _ctx.Add(PublicationData);
            await _ctx.SaveChangesAsync();
            return PublicationData;
        }

        public async Task<List<Publication>> GetAllPublicationsAsync()
        {
            List<Publication> publications = await _ctx.Publications
                .ToListAsync();
            return publications;
        }

        public async Task<Publication> GetPublicationByIdAsync(int Id)
        {
            Publication publication = await _ctx.Publications
                .FirstOrDefaultAsync((publication) => publication.PublicationId == Id);
            return publication;
        }

        public async Task<List<Author>> GetAllPublicationAuthorsAsync(int Id)
        {
            List<Author> publicationAuthors = await _ctx.Publications
                .Where((publication) => publication.PublicationId == Id)
                .Include((publication) => publication.Authors)
                .SelectMany((publication) => publication.Authors)
                .ToListAsync();
            return publicationAuthors;
        }

        public async Task<List<Blog>> GetAllPublicationBlogsAsync(int Id)
        {
            List<Blog> publicationBlogs = await _ctx.Publications
                .Where((publication) => publication.PublicationId == Id)
                .Include((publication) => publication.Blogs)
                .SelectMany((publication) => publication.Blogs)
                .ToListAsync();
            return publicationBlogs;
        }

        public async Task<Publication> DeletePublicationByIdAsync(int Id)
        {
            Publication publicationToDelete = await _ctx.Publications
                .FirstOrDefaultAsync((publication) => publication.PublicationId == Id);
            _ctx.Publications.Remove(publicationToDelete);
            return publicationToDelete;
        }

        public async Task<Publication> UpdatePublicationAsync(Publication PublicationData)
        {
            _ctx.Publications.Update(PublicationData);
            await _ctx.SaveChangesAsync();
            return PublicationData;
        }
    }
}
