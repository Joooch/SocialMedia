using AutoMapper;
using SocialMedia.Application.App.ChatMessages.Responses;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;
using SocialMedia.Application.Extensions;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class ChatMessagesRepository : BaseRepository<ChatMessageEntity>, IChatMessagesRepository
    {
        private readonly IMapper _mapper;
        private readonly IFriendsRepository _friendsRepository;

        public ChatMessagesRepository(ApplicationDbContext context, IFriendsRepository friendsRepository, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _friendsRepository = friendsRepository;
        }

        public async Task<PaginatedResult<ChatMessageDto>> GetByUserId(Guid userId, Guid targetId, PagedRequest pagedRequest)
        {
            return await EntitySet
                .Where(c => (c.TargetId == userId && c.OwnerId == targetId) || (c.TargetId == targetId && c.OwnerId == userId))
                .ApplyPaginatedResultAsync<ChatMessageEntity, ChatMessageDto>(pagedRequest, _mapper);
        }

        public async Task<PaginatedResult<FriendWithLastChatMessageDto>> GetByUserId(Guid userId, PagedRequest pagedRequest)
        {

            var friends = (await _friendsRepository.EntitySet
                .Where(c => c.UserId == userId)
                .Select(c => c.Friend)
                .ApplyPaginatedResultAsync<ProfileEntity, ProfileProtectedDto>(pagedRequest, _mapper))
                .Items;

            var friendIds = friends.Select(c => new Guid(c.UserId)).ToList();

            var messagesFrom = EntitySet.
                Where(c => c.OwnerId == userId && friendIds.Contains(c.TargetId))
                .DistinctBy(c => c.TargetId)
                .Select(c => c.Content);

            var messagesTo = EntitySet.
                Where(c => c.TargetId == userId && friendIds.Contains(c.OwnerId))
                .DistinctBy(c => c.OwnerId)
                .Select(c => c.Content);

            return null;
        }
    }
}
