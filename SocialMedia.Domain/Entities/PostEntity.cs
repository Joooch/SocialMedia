using SocialMedia.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain.Entities
{
    [Table("Posts")]
    public class PostEntity : BaseEntity, ITimedEntity
    {
        public DateTime CreatedAt { get; set; }


        [Required, MinLength(2), MaxLength(1024 * 4)]
        public string Content { get; set; }

        public ICollection<ImageEntity>? Images { get; set; }
        public ICollection<CommentEntity>? Comments { get; set; }

        public Guid OwnerId { get; set; }
        public ProfileEntity Owner { get; set; }
    }
}
