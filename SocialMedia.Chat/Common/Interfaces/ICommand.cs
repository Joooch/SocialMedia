using System.Text.Json.Nodes;

namespace SocialMedia.Chat.Common.Interfaces
{
    public interface ICommand
    {
        public string Type { get; set; }
    }
}
