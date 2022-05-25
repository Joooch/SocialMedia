using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.App.Posts.Responses
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public ICollection<string>? Images { get; set; }
        public ProfileProtectedDto Owner { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
