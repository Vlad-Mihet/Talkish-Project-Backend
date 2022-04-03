using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talkish.Domain.Models
{
    public class Follower
    {
        [Key]
        public int FollowerId { get; set; }

        [ForeignKey("User")]
        public int FollowingUserId { get; set; }

        [ForeignKey("User")]
        public int FollowedUserId { get; set; }
    }
}
