using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Common.Dtos.User
{
    public class ProfileDto
    {
        public string FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
