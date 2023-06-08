using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class LikesRepository : BaseRepository<LikeEntity>, ILikesRepository
    {
        public LikesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<LikeEntity> GetById(Guid likeId)
        {
            return await EntitySet.FirstAsync(c => c.Id == likeId);
        }

        public async Task<IList<ProfileEntity>> GetLatestByPostId(Guid postId, int max = 3)
        {
            return await EntitySet
                .Include(c => c.User)
                .Where(l => l.PostId == postId)
                .Select(l => l.User)
                .Take(max)
                .ToListAsync();
        }

        public async Task<int> GetCountByPostId(Guid postId)
        {
            return await EntitySet
                .Where(l=>l.PostId == postId)
                .CountAsync();
        }

        public async Task<LikeEntity?> GetLikeFromUser(Guid ownerId, Guid postId)
        {
            return await EntitySet.FirstOrDefaultAsync(c=>c.UserId == ownerId && c.PostId == postId);
        }
    }
}
