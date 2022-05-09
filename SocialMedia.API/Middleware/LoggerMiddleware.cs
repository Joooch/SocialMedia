namespace SocialMedia.API.Middleware
{
    public class LoggerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggerMiddleware>();
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            LogRequest(context);

            await _next(context);
        }

        private void LogRequest(HttpContext context)
        {
            _logger.LogInformation($"Http request information:\n" +
                $"\tSchma: {context.Request.Scheme}\n" +
                $"\tHost: {context.Request.Host}\n" +
                $"\tPath: {context.Request.Path}\n");
        }
    }

}
