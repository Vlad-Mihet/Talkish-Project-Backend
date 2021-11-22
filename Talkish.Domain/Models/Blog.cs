using System.Collections.Generic;

namespace Talkish.API.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int ReadingTime { get; set; }

        public List<Topic> Topics { get; set; }

        public Author Author { get; set; }
    }
}
