using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {
        public Task<Profile?> GetByUser(User user);
        public Task<bool> IsExists(User user);

    }
}
