using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetByEmail(string email);
        public Task<User?> GetById(string id);
        public Task<bool> IsConfirmed(User user);
    }
}
