using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Common.Dtos.User
{
    public class ProfileUpdateDto
    {
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
