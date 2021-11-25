namespace Talkish.API.DTOs
{
    class BlogByTopic
    {
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public BlogAuthorDTO Author { get; set; }
    }
}
