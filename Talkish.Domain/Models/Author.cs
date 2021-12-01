using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talkish.Domain.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
