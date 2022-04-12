using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talkish.Domain.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [Required]
        [Display(Name = "Topic Name")]
        public string Name { get; set; }

        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
