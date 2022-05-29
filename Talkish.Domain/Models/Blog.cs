using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Draft Story")]
        public bool IsDraft { get; set; } = false;

        [DataType(DataType.DateTime)]
        [Display(Name = "Published At")]
        public DateTime PublishedAt { get; set; } = DateTime.Now;

        public int AuthorId { get; set; }

        [Display(Name = "Blog Reading Time")]
        public int ReadingTime { get; set; }

        public List<Topic> Topics { get; set; } = new List<Topic>();

        public Author Author { get; set; }
    }
}
