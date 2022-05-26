using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;
using SocialMedia.Application.Extensions;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class FriendsRepository : BaseRepository<FriendsPairEntity>, IFriendsRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        public FriendsRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<ProfileProtectedDto>> GetFriendsByUserId(Guid userId, PagedRequest page)
        {
            return await EntitySet
                .Where(c => c.UserId == userId)
                .Select(c => c.Friend)
                .ApplyPaginatedResultAsync<ProfileEntity, ProfileProtectedDto>(page, _mapper);
        }

        public async Task<int> GetFriendsCountByUserId(Guid userId)
        {
            return await EntitySet.Where(c => c.UserId == userId).CountAsync();
        }

        public async Task<FriendStatus> GetFriendStatus(Guid userId, Guid friendId)
        {
            if (await EntitySet.AnyAsync(c => c.UserId == userId && c.FriendId == friendId))
            {
                return FriendStatus.Friend;
            }

            var requestEntitySet = _dbContext.Set<FriendsRequestEntity>();
            if (await requestEntitySet.AnyAsync(c => c.UserId == userId && c.TargetId == friendId))
            {
                return FriendStatus.Pending;
            }

            return FriendStatus.NotFriend;
        }


        private async Task CreateFriendsPair(Guid userId, Guid friendId)
        {
            var pair1 = new FriendsPairEntity()
            {
                UserId = userId,
                FriendId = friendId
            };
            var pair2 = new FriendsPairEntity() // reverse
            {
                UserId = friendId,
                FriendId = userId
            };

            Add(pair1);
            Add(pair2);

            await SaveAsync();
        }
        public async Task SendFriendRequest(Guid userId, Guid friendId)
        {
            var entitySet = _dbContext.Set<FriendsRequestEntity>();
            var pair = await entitySet.FirstOrDefaultAsync(c => c.UserId == friendId && c.TargetId == userId); // check if target has the same request

            if (pair != null)
            {
                entitySet.Remove(pair);

                await CreateFriendsPair(userId, friendId);
            }
            else
            {
                entitySet.Add(new FriendsRequestEntity()
                {
                    UserId = userId,
                    TargetId = friendId
                });

                await SaveAsync();
            }
        }

        /*public async Task AcceptFriendRequest(Guid userId, Guid friendId)
        {
            var entitySet = _dbContext.Set<FriendsRequestEntity>();
            var pair = await entitySet.FirstOrDefaultAsync(c => c.UserId == userId && c.TargetId == friendId);

            if (pair != null)
            {
                entitySet.Remove(pair);
                
                await CreateFriendsPair(userId, friendId);
            }
            else
            {
                throw new Exception("Invalid target");
            }
        }*/
        public async Task DeclineFriendRequest(Guid userId, Guid friendId)
        {
            var entitySet = _dbContext.Set<FriendsRequestEntity>();
            var pair = await entitySet.FirstOrDefaultAsync(c => c.UserId == userId && c.TargetId == friendId);

            if (pair != null)
            {
                entitySet.Remove(pair);
            }
            else
            {
                throw new Exception("Invalid target");
            }
        }
    }
}
