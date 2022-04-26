using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Task<Profile?> GetByUser(User user)
        {
            return EntitySet.FirstOrDefaultAsync(x => x.User == user);
        }
        public Task<Profile?> GetByUserId(string id)
        {
            return EntitySet.FirstOrDefaultAsync(x => x.UserId.ToString() == id);
        }

        public Task<bool> IsExists(User user)
        {
            return EntitySet.AnyAsync(x => x.User == user);
        }
    }
}
