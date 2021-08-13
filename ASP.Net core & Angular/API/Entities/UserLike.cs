using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Likes")]
    public class UserLike
    {
        public AppUser User { get; set; }

        public int UserId { get; set; }

        public AppUser LikedUser { get; set; }
        public int LikedUserId { get; set; }
    }
}