using SocialMedia.Infrastructure;

namespace SocialMedia.API.Middleware
{
    public class DbTransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public DbTransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            if (context.Request.Method == HttpMethod.Get.Method)
            {
                await _next(context);
                return;
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                await _next(context);

                await dbContext.Database.CommitTransactionAsync();
            }
        }
    }
}
