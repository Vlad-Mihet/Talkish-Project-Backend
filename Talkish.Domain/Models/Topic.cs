using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkish.Domain.Models
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
