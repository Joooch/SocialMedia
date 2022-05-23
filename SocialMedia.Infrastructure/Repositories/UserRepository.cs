using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<UserEntity?> GetByEmail(string email)
        {
            return Get(x => x.Email == email);
        }

        public Task<UserEntity?> GetById(string id)
        {
            return Get(x => x.Id.ToString() == id);
        }

        public Task<UserEntity?> GetById(Guid id)
        {
            return Get(x => x.Id == id);
        }

        public Task<bool> IsConfirmed(UserEntity user)
        {
            var profileRepository = new ProfileRepository(_dbContext);
            return profileRepository.IsExists(user);
        }
    }
}
