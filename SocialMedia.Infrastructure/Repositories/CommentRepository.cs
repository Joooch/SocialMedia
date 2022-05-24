using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PaginatedResult<CommentEntity>> GetPagedList(PagedRequest pagedRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
