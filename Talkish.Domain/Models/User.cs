using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talkish.Domain.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "User's Author Profile")]
        public Author AuthorProfile { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Users that are following this user")]
        public List<Follower> Followers { get; set; }

        public string IdentityId { get; set; }

        public BasicInfo BasicInfo { get; set; }
    }
}
