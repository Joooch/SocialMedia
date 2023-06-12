using SocialMedia.Application.App.Profiles.Responses;

namespace SocialMedia.Application.App.ChatMessages.Responses
{
    public class FriendWithLastChatMessageDto : ProfileProtectedDto
    {
        public ChatMessageDto Message { get; set; }
    }
}
