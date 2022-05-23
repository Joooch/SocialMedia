using SocialMedia.Domain.Entities;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface IPostsRepository : IRepository<PostEntity>
    {
        Task<IList<PostEntity>> GetPostsByUserId(Guid userId, PagedRequest pagedRequest);

    }
}
