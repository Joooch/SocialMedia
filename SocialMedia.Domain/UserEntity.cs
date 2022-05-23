using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Token { get; set; }

        public ProfileEntity? Profile { get; set; }
    }
}
