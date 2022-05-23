using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain;

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
        public Task<ProfileEntity?> GetByUserId(Guid id)
        {
            return EntitySet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> IsExists(UserEntity user)
        {
            return EntitySet.AnyAsync(x => x.User == user);
        }
    }
}
