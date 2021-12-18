using System.ComponentModel.DataAnnotations;

namespace Talkish.API.DTOs
{
    public class UpdateTopicDTO
    {
        [Required]
        [Display(Name = "Topic")]
        public string Name { get; set; }
    }
}
