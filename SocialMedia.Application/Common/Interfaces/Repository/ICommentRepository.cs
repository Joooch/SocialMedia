using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.Common.Models;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface ICommentRepository : IRepository<CommentEntity>
    {
        public Task<PaginatedResult<CommentDto>> GetCommentsByUserId(Guid userId, PagedRequest pagedRequest);
    }
}
