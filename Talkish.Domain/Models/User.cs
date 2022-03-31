using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talkish.Domain.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "User's Author Profile")]
        public Author AuthorProfile { get; set; }

        [Display(Name = "Users that this user is following")]
        public List<User> Following { get; set; }

        [Display(Name = "Users that are following this user")]
        public List<User> Followers { get; set; }
    }
}
