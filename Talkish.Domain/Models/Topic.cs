using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talkish.API.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [Required]
        [Display(Name = "Topic")]
        public string Name { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
