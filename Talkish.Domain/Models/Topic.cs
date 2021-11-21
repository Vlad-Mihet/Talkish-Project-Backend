using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkish.Domain.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
