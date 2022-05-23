using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    [Table("Friends")]
    public class FriendsPairEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public ProfileEntity User { get; set; }


        public Guid FriendId { get; set; }
        public ProfileEntity Friend { get; set; }
    }
}
