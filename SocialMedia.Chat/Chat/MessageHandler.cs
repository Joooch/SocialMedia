using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SocialMedia.Chat.Chat.Commands;
using SocialMedia.Chat.Common;
using SocialMedia.Chat.Entities;
using System.Net.WebSockets;
using System.Text;

namespace SocialMedia.Chat.Chat
{
    public class MessageHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private async Task<string?> ReadStringAsync(ChatClient client)
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using var ms = new MemoryStream();

            WebSocketReceiveResult result;
            do
            {
                result = await client.ReceiveAsync(buffer);
                ms.Write(buffer.Array, buffer.Offset, result.Count);
            }
            while (!result.EndOfMessage);

            ms.Seek(0, SeekOrigin.Begin);
            if (result.MessageType != WebSocketMessageType.Text)
            {
                return null;
            }

            using (var reader = new StreamReader(ms, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async Task HandleCommand(ChatClient client, BaseCommand command)
        {
            // todo: parse automatically
            if (command.Type == "SendMessage")
            {
                var service = _serviceProvider.GetRequiredService<SendMessageCommandHandler>();
                var payload = command.Payload;
                await service.HandleCommand(client, payload.ToObject<SendMessageCommand>()!);
            }

            return;
        }

        public async Task Listen(ChatClient client)
        {
            while (client.Alive)
            {
                var message = await ReadStringAsync(client);
                Console.WriteLine(message);
                var command = JsonConvert.DeserializeObject<BaseCommand>(message);

                if (command is null)
                {
                    break;
                }

                Console.WriteLine("Received command: %s", command.Type);
                Console.WriteLine("Full message: %s", message);
                await HandleCommand(client, command);
            }

            Console.WriteLine("done listening??");
        }
    }
}
