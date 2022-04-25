using Microsoft.IdentityModel.Tokens;
using SocialMedia.API.Extensions;
using SocialMedia.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await attachAccountToContext(context, repository, authOptions, token);

            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, IUserRepository repository, AuthOptions authOptions, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, authOptions.TokenValidation, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var email = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value;

                var user = await repository.GetByEmail(email);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(new List<Claim>()
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                    }, "basic");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            catch { }
        }
    }
}
