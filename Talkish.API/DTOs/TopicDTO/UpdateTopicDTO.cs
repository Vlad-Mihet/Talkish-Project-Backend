using System.ComponentModel.DataAnnotations;

namespace Talkish.API.DTOs
{
    public class UpdateTopicDTO
    {
        [Required]
        [Key]
        [Display(Name = "Topic Id")]
        public int TopicId { get; set; }

        [Required]
        [Display(Name = "Topic")]
        public string Name { get; set; }
    }
}
