namespace Talkish.API.DTOs;
{
    public class UserWithBioDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }
    }
}
