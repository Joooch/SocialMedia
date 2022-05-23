using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface IImageRepository : IRepository<ImageEntity>
    {
        public Task<ImageEntity> GetById(Guid imageId);
    }
}
