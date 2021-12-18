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
                .Include((publication) => publication.Owner)
                .ToListAsync();
            return publications;
        }

        public async Task<Publication> GetPublicationByIdAsync(int Id)
        {
            Publication publication = await _ctx.Publications
                .Include((publication) => publication.Owner)
                .FirstOrDefaultAsync((publication) => publication.PublicationId == Id);
            return publication;
        }

        public async Task<Publication> GetPublicationWithBlogsByIdAsync(int Id)
        {
            Publication publication = await _ctx.Publications
                .Include((publication) => publication.Owner)
                .Include((publication) => publication.Blogs)
                .ThenInclude((blog) => blog.Author)
                .FirstOrDefaultAsync((publication) => publication.PublicationId == Id);
            return publication;
        }

        public async Task<List<Author>> GetAllPublicationAuthorsAsync(int Id)
        {
            Publication publication = await _ctx.Publications
                .Include((publication) => publication.Authors)
                .FirstOrDefaultAsync((publication) => publication.PublicationId == Id);

            if (publication == null)
            {
                return null;
            }

            List<Author> publicationAuthors = publication.Authors.ToList();
            return publicationAuthors;
        }

        public async Task<List<Blog>> GetAllPublicationBlogsAsync(int Id)
        {
            Publication publication = await _ctx.Publications
                .Include((publication) => publication.Blogs)
                .FirstOrDefaultAsync((publication) => publication.PublicationId == Id);

            if (publication == null)
            {
                return null;
            }

            List<Blog> publicationBlogs = publication.Blogs.ToList();
            return publicationBlogs;
        }

        public async Task<Publication> DeletePublicationByIdAsync(int Id)
        {
            Publication publicationToDelete = await _ctx.Publications
                .Include((publication) => publication.Owner)
                .FirstOrDefaultAsync((publication) => publication.PublicationId == Id);
            
            if (publicationToDelete == null)
            {
                return null;
            }
            
            _ctx.Publications.Remove(publicationToDelete);
            await _ctx.SaveChangesAsync();
            return publicationToDelete;
        }

        public async Task<Publication> UpdatePublicationAsync(int PublicationId, Publication PublicationData)
        {
            Publication publication = await _ctx.Publications.FirstOrDefaultAsync((publication) => publication.PublicationId == PublicationId);
           
            if (publication == null)
            {
                return null;
            }
            
            publication.Name = PublicationData.Name;
            _ctx.Publications.Update(publication);
            await _ctx.SaveChangesAsync();
            return publication;
        }

        public async Task<Publication> AddBlogToPublicationAsync(int PublicationId, int BlogId)
        {
            Blog blog = await _ctx.Blogs
                .FirstOrDefaultAsync((blog) => blog.BlogId == BlogId);
            Publication publication = await _ctx.Publications
                .Include((publication) => publication.Blogs)
                .FirstOrDefaultAsync((publication) => publication.PublicationId == PublicationId);

            if (publication == null || blog == null)
            {
                return null;
            }
            
            publication.Blogs.Add(blog);
            await _ctx.SaveChangesAsync();
            return publication;
        }

        public async Task<Publication> AddAuthorToPublicationAsync(int PublicationId, int AuthorId)
        {
            Author author = await _ctx.Authors
                .FirstOrDefaultAsync((author) => author.AuthorId == AuthorId);
            Publication publication = await _ctx.Publications
                .Include((publication) => publication.Authors)
                .FirstOrDefaultAsync((publication) => publication.PublicationId == PublicationId);
            
            if (publication == null || author == null)
            {
                return null;
            }
            
            publication.Authors.Add(author);
            await _ctx.SaveChangesAsync();
            return publication;
        }
    }
}
