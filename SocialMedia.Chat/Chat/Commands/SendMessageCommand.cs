using SocialMedia.Chat.Common.Interfaces;
using SocialMedia.Chat.Entities;

namespace SocialMedia.Chat.Chat.Commands
{
    public class SendMessageCommand
    {
        public Guid TargetId { get; set; }
        public string Message { get; set; }
    }

    public class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
    {
        private readonly ChatApp _chatApp;
        public SendMessageCommandHandler(ChatApp chatApp) {
            _chatApp = chatApp;
        }

        public async Task HandleCommand(ChatClient chatClient, SendMessageCommand payload)
        {
            Console.WriteLine("Received SendMessage command from: " + chatClient.ToString());
            Console.WriteLine(payload.TargetId);
            Console.WriteLine(payload.Message);

            var (targetId, targetClient) = _chatApp.Clients.FirstOrDefault(c=>c.Key == payload.TargetId);
            if(targetClient is not null)
            {
                await targetClient.SendStringAsync(payload.Message);
            }
            else
            {
                Console.WriteLine("Target not found!");
            }
        }
    }
}
