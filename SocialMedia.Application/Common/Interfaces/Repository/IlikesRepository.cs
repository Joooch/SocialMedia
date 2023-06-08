using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface ILikesRepository : IRepository<LikeEntity>
    {
        public Task<LikeEntity> GetById(Guid likeId);
        public Task<int> GetCountByPostId(Guid postId);
        public Task<IList<ProfileEntity>> GetLatestByPostId(Guid postId, int max = 3);
        public Task<LikeEntity?> GetLikeFromUser(Guid ownerId, Guid postId);
    }
}
