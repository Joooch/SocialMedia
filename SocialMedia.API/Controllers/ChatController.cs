using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Extensions;
using SocialMedia.Chat;

namespace SocialMedia.API.Controllers
{
    public class ChatController : BaseController
    {
        private readonly ChatApp _chatApp;

        public ChatController(ChatApp chatApp)
        {
            _chatApp = chatApp;
        }

        [Route("WebSocket")]
        [NonAction]
        public async Task ChatWebsocket()
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                throw new BadHttpRequestException("Web socket expected");
            }

            CancellationToken CancelToken = HttpContext.RequestAborted;

            Console.WriteLine("TEST EX:");

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await _chatApp.HandleWebSocket(webSocket, HttpContext.User.GetUserId(), CancelToken);

            Console.WriteLine("test.... 1");
        }
    }
}
