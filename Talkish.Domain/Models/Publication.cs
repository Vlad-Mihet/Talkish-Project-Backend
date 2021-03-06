using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talkish.Domain.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Publication's name must be longer than 3 characters, but shorter than 100")]
        [Display(Name = "Publication Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "Publication's description must be longer than 20 characters, but shorter than 200")]
        [Display(Name = "Publication Description")]
        public string Description { get; set; }

        [ForeignKey("Author")]
        public int OwnerId { get; set; }
        
        // The owner will be a user identity
        // in the future
        // The owner created a relationship in our db
        public Author Owner { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();

        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
