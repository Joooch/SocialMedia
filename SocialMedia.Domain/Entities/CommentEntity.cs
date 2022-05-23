using SocialMedia.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain.Entities
{
    public class CommentEntity : BaseEntity, ITimedEntity
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }


        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public ProfileEntity Owner { get; set; }


        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public PostEntity Post { get; set; }
    }
}
