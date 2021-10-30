using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkish.Domain.Models
{
    public class Blog
    {
        /* TODO: */
        // - Add Auto-Generated Id Here
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
    }
}
