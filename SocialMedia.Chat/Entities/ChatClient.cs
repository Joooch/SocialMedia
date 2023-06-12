using Newtonsoft.Json;
using SocialMedia.Chat.Common.Interfaces;
using System.Net.WebSockets;
using System.Text;

namespace SocialMedia.Chat.Entities
{
    public class ChatClient
    {
        private readonly WebSocket _socket;
        private readonly CancellationToken _cancellationToken;
        public Guid UserId { get; private set; }

        public ChatClient(WebSocket socket, Guid userId, CancellationToken cancellationToken)
        {
            _socket = socket;
            _cancellationToken = cancellationToken;
            UserId = userId;
        }

        public bool Alive
        {
            get
            {
                return !_socket.CloseStatus.HasValue && !_cancellationToken.IsCancellationRequested;
            }
        }

        public Task SendStringAsync(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return _socket.SendAsync(segment, WebSocketMessageType.Text, true, _cancellationToken);
        }

        public async Task SendCommandAsync(ICommand command)
        {
            var obj = JsonConvert.SerializeObject(command);
            await SendStringAsync(obj);
        }

        public async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer)
        {
            _cancellationToken.ThrowIfCancellationRequested();

            return await _socket.ReceiveAsync(buffer, _cancellationToken);
        }
    }
}
