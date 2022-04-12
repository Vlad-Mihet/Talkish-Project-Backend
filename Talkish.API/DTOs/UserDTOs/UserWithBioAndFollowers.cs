using System.Collections.Generic;

namespace Talkish.API.DTOs
{
    public class UserWithBioAndFollowers
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public List<FollowerDTO> Followers { get; set; }
    }
}
