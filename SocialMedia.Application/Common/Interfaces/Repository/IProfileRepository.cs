using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface IProfileRepository : IRepository<ProfileEntity>
    {
        public Task<ProfileEntity?> GetByUser(UserEntity user);
        public Task<ProfileEntity?> GetByUserId(Guid id);
        public Task<bool> IsExists(UserEntity user);

    }
}
