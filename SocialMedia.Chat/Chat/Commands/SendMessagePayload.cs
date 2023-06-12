using AutoMapper;
using SocialMedia.Application.App.ChatMessages.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Chat.Common;
using SocialMedia.Chat.Common.Interfaces;
using SocialMedia.Chat.Entities;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Chat.Chat.Commands
{
    public class SendMessagePayload
    {
        public Guid TargetId { get; set; }
        public string Message { get; set; }
    }

    public class SendMessageResponse : BaseCommandResponse<ChatMessageDto>
    {
        public SendMessageResponse(ChatMessageDto payload) : base("Message", payload)
        {
        }
    }

    public class SendMessageCommandHandler : ICommandHandler<SendMessagePayload>
    {
        private readonly ChatApp _chatApp;
        private readonly IChatMessagesRepository _chatMessagesRepository;
        private readonly IMapper _mapper;

        public SendMessageCommandHandler(ChatApp chatApp, IChatMessagesRepository chatMessagesRepository, IMapper mapper)
        {
            _chatApp = chatApp;
            _chatMessagesRepository = chatMessagesRepository;
            _mapper = mapper;
        }

        public async Task HandleCommand(ChatClient chatClient, SendMessagePayload payload)
        {
            Console.WriteLine("Received SendMessage command from: " + chatClient.ToString());
            Console.WriteLine(payload.TargetId);
            Console.WriteLine(payload.Message);

            var chatMessage = new ChatMessageEntity();
            chatMessage.Content = payload.Message;
            chatMessage.OwnerId = chatClient.UserId;
            chatMessage.TargetId = payload.TargetId;
            chatMessage.CreatedAt = DateTime.Now;

            _chatMessagesRepository.Add(chatMessage);
            await _chatMessagesRepository.SaveAsync();

            var responseCommand = new BaseCommandResponse<ChatMessageDto>("Message", _mapper.Map<ChatMessageDto>(chatMessage));

            await chatClient.SendCommandAsync(responseCommand);

            var (targetId, targetClient) = _chatApp.Clients.FirstOrDefault(c => c.Key == payload.TargetId);
            if (targetClient is not null)
            {
                await targetClient.SendCommandAsync(responseCommand);
            }
            else
            {
                Console.WriteLine("Target not found!");
            }
        }
    }
}
