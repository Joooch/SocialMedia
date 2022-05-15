using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    public class ProfileRepository : BaseRepository<ProfileEntity>, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Task<ProfileEntity?> GetByUser(UserEntity user)
        {
            return EntitySet.FirstOrDefaultAsync(x => x.User == user);
        }
        public Task<ProfileEntity?> GetByUserId(string id)
        {
            return EntitySet.FirstOrDefaultAsync(x => x.UserId.ToString() == id);
        }

        public Task<bool> IsExists(UserEntity user)
        {
            return EntitySet.AnyAsync(x => x.User == user);
        }
    }
}
