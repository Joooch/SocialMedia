using MediatR;
using SocialMedia.Application.App.ChatMessages.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.App.ChatMessages.Queries
{
    public class GetFriendsChatMessagesQuery : IRequest<PaginatedResult<FriendWithLastChatMessageDto>>
    {
        public Guid UserId { get; set; }
        public PagedRequest? PageInfo { get; set; }
    }

    public class GetFriendsChatMessagesQueryHandler : IRequestHandler<GetFriendsChatMessagesQuery, PaginatedResult<FriendWithLastChatMessageDto>>
    {
        private readonly IChatMessagesRepository _chatMessagesRepository;

        public GetFriendsChatMessagesQueryHandler(IChatMessagesRepository chatMessagesRepository)
        {
            _chatMessagesRepository = chatMessagesRepository;
        }

        public async Task<PaginatedResult<FriendWithLastChatMessageDto>> Handle(GetFriendsChatMessagesQuery request, CancellationToken cancellationToken)
        {
            var page = request.PageInfo ?? new PagedRequest() { PageSize = 10 };
            var messages = await _chatMessagesRepository.GetByUserId(request.UserId, page);

            return messages;
        }
    }
}
