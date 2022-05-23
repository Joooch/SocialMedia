using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config["DefaultConnection"]);
            });

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IProfileRepository), typeof(ProfileRepository));
            services.AddScoped(typeof(IPostsRepository), typeof(PostsRepository));
            services.AddScoped(typeof(IImageRepository), typeof(ImageRepository));

            return services;
        }
    }
}
