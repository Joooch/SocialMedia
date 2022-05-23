using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<ImageEntity>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ImageEntity> GetById(Guid imageId)
        {
            return await EntitySet.FirstAsync(c => c.Id == imageId);
        }
    }
}
