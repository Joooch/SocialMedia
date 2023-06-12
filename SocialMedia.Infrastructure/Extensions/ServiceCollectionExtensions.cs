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
                options.UseSqlServer(config["DefaultConnection"], builder => builder.MigrationsAssembly("SocialMedia.API"));
            });

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IProfileRepository), typeof(ProfileRepository));
            services.AddScoped(typeof(IPostsRepository), typeof(PostsRepository));
            services.AddScoped(typeof(IImageRepository), typeof(ImageRepository));
            services.AddScoped(typeof(ILikesRepository), typeof(LikesRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
            services.AddScoped(typeof(IFriendsRepository), typeof(FriendsRepository));
            services.AddScoped(typeof(IChatMessagesRepository), typeof(ChatMessagesRepository));

            return services;
        }
    }
}
