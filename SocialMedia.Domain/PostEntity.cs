using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    [Table("Posts")]
    public class PostEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }


        [Required, MinLength(2), MaxLength(1024 * 4)]
        public string Content { get; set; }

        public ICollection<ImageEntity>? Images { get; set; }

        public Guid UserId { get; set; }
        public ProfileEntity UserOwner { get; set; }
    }
}
