using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    public class CommentEntity : BaseEntity
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ProfileEntity User { get; set; }


        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public PostEntity Post { get; set; }
    }
}
