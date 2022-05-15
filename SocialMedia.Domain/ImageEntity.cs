using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    public class ImageEntity : BaseEntity
    {
        [Key]
        public Guid ImageId { get; set; }


        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public PostEntity Post { get; set; }
    }
}
