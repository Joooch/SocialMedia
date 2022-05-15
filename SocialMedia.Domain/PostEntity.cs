using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    [Table("Posts")]
    public class PostEntity : BaseEntity
    {
        [Key]
        public Guid PostId;


        [Required, MinLength(2), MaxLength(200)]
        public string Title { get; set; }

        [Required, MinLength(2), MaxLength(1024 * 4)]
        public string Content { get; set; }

        public ICollection<ImageEntity> Images { get; set; }

        public UserEntity UserOwner { get; set; }
    }
}
