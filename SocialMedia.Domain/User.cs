using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Domain
{
    public class User : BaseEntity
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Token { get; set; }

        public Profile? Profile { get; set; }
    }
}
