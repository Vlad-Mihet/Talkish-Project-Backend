namespace Talkish.API.DTOs
{
    public class AddBlogDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }
    }
}
