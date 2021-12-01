using System.ComponentModel.DataAnnotations;

namespace Talkish.API.DTOs
{
    public class UpdatePublicationDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Publication's name must be longer than 3 characters, but shorter than 100")]
        [Display(Name = "Publication Name")]
        public string Name { get; set; }
    }
}
