using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talkish.Domain.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [Display(Name = "Blog Title")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Blog Title must be longer than 3 characters, but shorter than 100")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Blog Content")]
        [StringLength(100000, MinimumLength = 30, ErrorMessage = "Blog content must be longer than 30 characters")]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Blog Reading Time")]
        public int ReadingTime { get; set; }

        public List<Topic> Topics { get; set; }

        public Author Author { get; set; }
    }
}
