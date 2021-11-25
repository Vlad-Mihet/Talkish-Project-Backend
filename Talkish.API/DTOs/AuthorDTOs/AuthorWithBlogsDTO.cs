using System.Collections.Generic;

namespace Talkish.API.DTOs
{
    public class AuthorWithBlogsDTO
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<AuthorBlogDTO> Blogs { get; set; } 
    }
}
