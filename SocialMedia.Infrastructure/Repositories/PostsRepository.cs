using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostsRepository : BaseRepository<PostEntity>, IPostsRepository
    {
        public PostsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<PostEntity>> GetPostsByUserId(Guid userId, PagedRequest pagedRequest)
        {
            var query = EntitySet;

            if (pagedRequest.LastId != null)
            {
                query.SkipWhile(c => c.Id != pagedRequest.LastId);
            }

            return await query
                .Take(pagedRequest.PageSize)
                .Include(c => c.UserOwner)
                .Include(c => c.Images)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
