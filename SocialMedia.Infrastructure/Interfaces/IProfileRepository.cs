using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IProfileRepository : IRepository<ProfileEntity>
    {
        public Task<ProfileEntity?> GetByUser(UserEntity user);
        public Task<ProfileEntity?> GetByUserId(string id);
        public Task<bool> IsExists(UserEntity user);

    }
}
