using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.ChatMessages.Queries;
using SocialMedia.Application.App.ChatMessages.Responses;
using SocialMedia.Application.Common.Models;
using SocialMedia.Chat;

namespace SocialMedia.API.Controllers
{
    public class ChatController : BaseController
    {
        private readonly ChatApp _chatApp;
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;

        public ChatController(ChatApp chatApp, IMediator mediator, IServiceProvider serviceProvider)
        {
            _chatApp = chatApp;
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        [Route("")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task ChatWebsocket()
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                throw new BadHttpRequestException("Web socket expected");
            }

            CancellationToken CancelToken = HttpContext.RequestAborted;

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _chatApp.HandleWebSocket(webSocket, HttpContext.RequestServices, HttpContext.User.GetUserId(), CancelToken);
        }

        [HttpGet("Messages/{targetId}")]
        public async Task<PaginatedResult<ChatMessageDto>> GetMessages(string targetId, [FromQuery] PagedRequest page)
        {
            var messages = await _mediator.Send(new GetUserChatMessagesQuery()
            {
                UserId = HttpContext.User.GetUserId(),
                TargetId = Guid.Parse(targetId),
                PageInfo = page
            });

            return messages;
        }

        [HttpGet("Friends")]
        public async Task<PaginatedResult<FriendWithLastChatMessageDto>> GetFriends([FromQuery] PagedRequest page)
        {
            var messages = await _mediator.Send(new GetFriendsChatMessagesQuery()
            {
                UserId = HttpContext.User.GetUserId(),
                PageInfo = page
            });

            return messages;
        }
    }
}
