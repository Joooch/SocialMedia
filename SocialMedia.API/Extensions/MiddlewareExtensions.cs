using SocialMedia.API.Middleware;

namespace SocialMedia.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDatabaseTransaction(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DbTransactionMiddleware>();
        }

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
