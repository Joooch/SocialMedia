namespace SocialMedia.Domain.Entities
{
    public class FriendsRequestEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public ProfileEntity User { get; set; }

        public Guid TargetId { get; set; }
        public ProfileEntity Target { get; set; }
    }
}
