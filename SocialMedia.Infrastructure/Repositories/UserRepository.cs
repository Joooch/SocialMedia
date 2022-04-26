using SocialMedia.Domain;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<User?> GetByEmail(string email)
        {
            return Get(x => x.Email == email);
        }

        public Task<User?> GetById(string id)
        {
            return Get(x => x.UserId.ToString() == id);
        }

        public Task<bool> IsConfirmed(User user)
        {
            var profileRepository = new ProfileRepository(_dbContext);
            return profileRepository.IsExists(user);
        }
    }
}
