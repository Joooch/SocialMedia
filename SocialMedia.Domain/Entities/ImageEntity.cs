using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain.Entities
{
    public class ImageEntity : BaseEntity
    {
        [ForeignKey(nameof(Post))]
        public Guid? PostId { get; set; }
        public PostEntity? Post { get; set; }

        
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public ProfileEntity Owner { get; set; }
    }
}
