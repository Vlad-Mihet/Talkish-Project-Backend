using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talkish.Domain.Models
{
    public class AuthUser
    {
        [Key]
        public int IdentityId { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
