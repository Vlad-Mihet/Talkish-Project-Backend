using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkish.Domain.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public List<Blog> blogs { get; set; }
    }
}
