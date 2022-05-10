using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SocialMedia.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return services.AddMediatR(assembly);
        }
    }
}
