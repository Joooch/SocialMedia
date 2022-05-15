using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    [Table("Friends")]
    public class FriendsPairEntity : BaseEntity
    {
        [Key]
        public Guid RelationId { get; set; }


        public Guid UserId { get; set; }
        public UserEntity User { get; set; }


        public Guid FriendId { get; set; }
        public UserEntity Friend { get; set; }
    }
}
