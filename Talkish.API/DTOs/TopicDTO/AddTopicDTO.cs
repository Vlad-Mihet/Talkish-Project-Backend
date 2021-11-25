using System.ComponentModel.DataAnnotations;

namespace Talkish.API.DTOs
{
    public class AddTopicDTO
    {
        [Required]
        [Display(Name = "Topic")]
        public string Name { get; set; }
    }
}
