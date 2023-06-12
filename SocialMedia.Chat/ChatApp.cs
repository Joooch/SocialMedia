using SocialMedia.Chat.Chat;
using SocialMedia.Chat.Entities;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace SocialMedia.Chat
{
    public class ChatApp
    {
        public ConcurrentDictionary<Guid, ChatClient> Clients { get; set; }
        public MessageHandler _messageHandler { get; set; }

        public ChatApp(MessageHandler messageHandler)
        {
            Clients = new ConcurrentDictionary<Guid, ChatClient>();
            _messageHandler = messageHandler;
        }

        public async Task HandleWebSocket(WebSocket ws, IServiceProvider serviceProvider, Guid UserId, CancellationToken cancellationToken)
        {
            if (Clients.ContainsKey(UserId))
            {
                Clients.TryRemove(UserId, out _);
            }

            var client = new ChatClient(ws, UserId, cancellationToken);
            Clients.TryAdd(UserId, client);

            await _messageHandler.Listen(client, serviceProvider);
        }
    }
}
