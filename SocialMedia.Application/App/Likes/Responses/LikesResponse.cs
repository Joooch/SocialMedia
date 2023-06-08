using SocialMedia.Application.App.Profiles.Responses;

namespace SocialMedia.Application.App.Likes.Responses
{
    public class LikesResponse
    {
        public bool HasLike { get; set; }
        public int TotalCount { get; set; }
        public IList<ProfileProtectedDto> OtherLikes { get; set; }
    }
}
