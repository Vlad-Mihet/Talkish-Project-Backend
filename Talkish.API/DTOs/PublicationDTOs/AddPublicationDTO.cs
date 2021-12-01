using System.ComponentModel.DataAnnotations;

namespace Talkish.API.DTOs
{
    public class AddPublicationDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Publication's name must be longer than 3 characters, but shorter than 100")]
        [Display(Name = "Publication Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Publication must have an owner")]
        public int OwnerId { get; set; }
    }
}
