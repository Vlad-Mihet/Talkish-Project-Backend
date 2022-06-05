using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talkish.API.DTOs
{
    public class AddBlogDTO
    {
        [Required]
        [Display(Name = "Blog Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Blog Content")]
        public string Content { get; set; }

        public bool IsDraft { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
    }
}
