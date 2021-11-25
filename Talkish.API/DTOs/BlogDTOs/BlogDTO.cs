using System.Collections.Generic;

namespace Talkish.API.DTOs
{
    public class BlogDTO
    {
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ReadingTime { get; set; }

        public List<TopicDTO> Topics { get; set; }

        public BlogAuthorDTO Author { get; set; }
    }
}
