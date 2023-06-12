using SocialMedia.Chat.Entities;

namespace SocialMedia.Chat.Common.Interfaces
{
    public interface ICommandHandler<T> where T : class
    {
        public Task HandleCommand(ChatClient chatClient, T command);
    }
}
