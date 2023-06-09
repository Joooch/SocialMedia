using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Chat.Chat;
using SocialMedia.Chat.Chat.Commands;

namespace SocialMedia.Chat.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddChat(this IServiceCollection services)
        {
            services.AddTransient<History>();
            services.AddTransient<MessageSender>();
            services.AddTransient<MessageHandler>();

            services.AddSingleton<ChatApp>();
            // services.AddTransient(p => p.GetServices(typeof(ICommandHandler)));
            services.AddTransient<SendMessageCommandHandler>();

            return services;
        }
    }
}
