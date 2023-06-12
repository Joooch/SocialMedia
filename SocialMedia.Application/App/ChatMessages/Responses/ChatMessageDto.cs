using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Application.App.ChatMessages.Responses
{
    public class ChatMessageDto : ITimedEntity
    {
        public string Id { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid OwnerId { get; set; }
    }
}
