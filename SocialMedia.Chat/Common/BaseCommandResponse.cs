using SocialMedia.Chat.Common.Interfaces;

namespace SocialMedia.Chat.Common
{
    public class BaseCommandResponse<T> : ICommand where T : class
    {
        public string Type { get; set; }
        public T Payload { get; set; }

        public BaseCommandResponse(string type, T payload)
        {
            Type = type;
            Payload = payload;
        }
    }
}
