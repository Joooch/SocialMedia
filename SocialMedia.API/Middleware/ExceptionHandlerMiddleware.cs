using Newtonsoft.Json;
using SocialMedia.API.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SocialMedia.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has occured");

                await CreateExceptionResponseAsync(context, ex);
            }
        }

        private static async Task CreateExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            string errorMessage;

            switch (ex)
            {
                case ValidationException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = ex.Message;
                    break;

                case CustomException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = ex.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = "An unexpected error has occured";
                    break;
            }

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = errorMessage
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
