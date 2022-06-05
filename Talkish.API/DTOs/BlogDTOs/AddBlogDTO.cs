using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talkish.API.DTOs
{
    public class AddBlogDTO
    {
        [Display(Name = "Blog Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Blog Content")]
        public string Content { get; set; }

        public bool IsDraft { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
    }
}
