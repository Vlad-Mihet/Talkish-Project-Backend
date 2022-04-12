using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talkish.Domain.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public List<Blog> Blogs { get; set; } = new List<Blog>();

        public User UserProfile { get; set; }

        public int UserId { get; set; }
    }
}
