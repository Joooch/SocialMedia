using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.API.Extensions;
using SocialMedia.Domain;
using SocialMedia.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialMedia.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class GoogleLoginController : BaseController
    {

        private IUserRepository _userRepository;
        private AuthOptions _authOptions;

        public GoogleLoginController(IUserRepository userRepository, AuthOptions authOptions)
        {
            _userRepository = userRepository;
            _authOptions = authOptions;
        }

        public class LoginRequest
        {
            public string Token { get; set; }
        }
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
                var user = await _userRepository.GetByEmail(payload.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Email = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName
                    };
                    user.Token = CreateToken(user);

                    _userRepository.Add(user);
                    await _userRepository.SaveAsync();

                    return Json(new { Token = user.Token });
                }
                else
                {
                    return Json(new { Token = CreateToken(user) });
                }
            }
            catch (InvalidJwtException)
            {
                return BadRequest("invalid token");
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = _authOptions.Сredentials,
                Audience = _authOptions.Audience,
                Issuer = _authOptions.Issuer,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
