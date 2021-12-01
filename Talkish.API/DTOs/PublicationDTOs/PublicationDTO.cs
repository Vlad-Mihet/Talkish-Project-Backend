using System.Collections.Generic;
using Talkish.Domain.Models;

namespace Talkish.API.DTOs
{
    public class PublicationDTO
    {
        public int PublicationId { get; set; }

        public string Name { get; set; }

        public Author Owner { get; set; }

        public List<Author> Authors { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
