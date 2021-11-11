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

        // Make Name Unique
        public string Name { get; set; }
    }
}
