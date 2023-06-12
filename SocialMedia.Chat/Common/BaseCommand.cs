using Newtonsoft.Json.Linq;
using SocialMedia.Chat.Common.Interfaces;

namespace SocialMedia.Chat.Common
{
    public class BaseCommand : ICommand
    {
        public string Type { get; set; }
        public JObject Payload { get; set; }
    }
}
