using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.Common.Models;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface IFriendsRepository : IRepository<FriendsPairEntity>
    {
        public Task<int> GetFriendsCountByUserId(Guid userId);
        public Task<PaginatedResult<ProfileProtectedDto>> GetFriendsByUserId(Guid userId, PagedRequest page);
        public Task<FriendStatus> GetFriendStatus(Guid userId, Guid friendId);

        public Task SendFriendRequest(Guid userId, Guid friendId);
        public Task DeclineFriendRequest(Guid userId, Guid friendId);
    }
}
