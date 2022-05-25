using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Application.App.Comments.Responses
{
    public class CommentDto : ITimedEntity
    {
        public string Id { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProfileDto Owner { get; set; }
    }
}
