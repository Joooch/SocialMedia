using SocialMedia.Application.App.Profiles.Responses;

namespace SocialMedia.Application.App.Users.Responses
{
    public class UserDto
    {
        public string Email { get; set; }
        public ProfileDto? Profile { get; set; }
    }
}
