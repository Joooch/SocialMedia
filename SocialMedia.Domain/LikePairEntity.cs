using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    [Table("Likes")]
    public class LikePairEntity : BaseEntity
    {
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public PostEntity Post { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
