using Microsoft.IdentityModel.Tokens;
using SocialMedia.API.Extensions;
using SocialMedia.Application.Common.Interfaces.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository repository, AuthOptions authOptions)
        {
            var splitted = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ");
            var token = splitted?.Last();
            if (token is not null)
            {
                await attachAccountToContext(context, repository, authOptions, token);
            }

            
            var wsToken = context.Request.Query.FirstOrDefault(q => q.Key == "token").Value.FirstOrDefault();
            if (wsToken is not null)
            {
                await attachAccountToContext(context, repository, authOptions, wsToken);
            }

            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, IUserRepository repository, AuthOptions authOptions, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, authOptions.TokenValidation, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.First(x => x.Type == AuthOptions.ClientClaimId).Value;

                var user = await repository.GetById(userId);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(new List<Claim>()
                    {
                        new Claim(AuthOptions.ClientClaimId, userId)
                    }, "Bearer");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            catch { }
        }
    }
}
