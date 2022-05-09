

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialMedia.API.Exceptions
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidUserException)
            {
                context.Result = new ContentResult
                {
                    Content = $"Пользователь '{context.Exception.Message}' не найден"
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
