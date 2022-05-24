using SocialMedia.Domain.Entities;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Models;
using SocialMedia.Application.App.Posts.Responses;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface IPostsRepository : IRepository<PostEntity>
    {
        public Task<PaginatedResult<PostDto>> GetPostsByUserId(Guid userId, PagedRequest pagedRequest);
    }
}
