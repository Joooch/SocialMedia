﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domain
{
    [Table("Profiles")]
    public class ProfileEntity : BaseEntity
    {
        [Key, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public UserEntity User { get; set; }


        [Required, MinLength(3), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string LastName { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string Address { get; set; }

        [Required, MinLength(5), MaxLength(30)]
        public string City { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string Region { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        public string Country { get; set; }
    }
}