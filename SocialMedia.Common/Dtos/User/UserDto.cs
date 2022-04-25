using SocialMedia.Domain;

namespace SocialMedia.Common.Dtos.User
{
    public class UserDto
    {
        public string Email { get; set; }
        public ProfileDto? Profile { get; set; }
    }
}
