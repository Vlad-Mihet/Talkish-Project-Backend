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
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Blog Content")]
        public string Content { get; set; }

        [Display(Name = "Draft Story")]
        public bool IsDraft { get; set; } = true;

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
