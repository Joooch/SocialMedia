using MediatR;
using SocialMedia.Application.App.ChatMessages.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.App.ChatMessages.Queries
{
    public class GetUserChatMessagesQuery : IRequest<PaginatedResult<ChatMessageDto>>
    {
        public Guid UserId { get; set; }
        public Guid TargetId { get; set; }
        public PagedRequest? PageInfo { get; set; }
    }

    public class GetUserChatMessagesQueryHandler : IRequestHandler<GetUserChatMessagesQuery, PaginatedResult<ChatMessageDto>>
    {
        private readonly IChatMessagesRepository _chatMessagesRepository;

        public GetUserChatMessagesQueryHandler(IChatMessagesRepository chatMessagesRepository)
        {
            _chatMessagesRepository = chatMessagesRepository;
        }

        public async Task<PaginatedResult<ChatMessageDto>> Handle(GetUserChatMessagesQuery request, CancellationToken cancellationToken)
        {
            var page = request.PageInfo ?? new PagedRequest() { PageSize = 10 };
            var messages = await _chatMessagesRepository.GetByUserId(request.UserId, request.TargetId, page);

            return messages;
        }
    }
}
