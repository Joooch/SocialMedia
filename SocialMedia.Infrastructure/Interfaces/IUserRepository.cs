using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        public Task<UserEntity?> GetByEmail(string email);
        public Task<UserEntity?> GetById(string id);
        public Task<UserEntity?> GetById(Guid id);
        public Task<bool> IsConfirmed(UserEntity user);
    }
}
