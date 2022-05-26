using SocialMedia.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
