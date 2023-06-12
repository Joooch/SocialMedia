using SocialMedia.Domain.Entities;
using SocialMedia.Application.Common.Models;
using SocialMedia.Application.App.ChatMessages.Responses;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface IChatMessagesRepository : IRepository<ChatMessageEntity>
    {
        public Task<PaginatedResult<ChatMessageDto>> GetByUserId(Guid userId, Guid targetId, PagedRequest pagedRequest);
        public Task<PaginatedResult<FriendWithLastChatMessageDto>> GetByUserId(Guid userId, PagedRequest pagedRequest);
    }
}
