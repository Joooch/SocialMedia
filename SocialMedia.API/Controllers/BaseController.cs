using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Exceptions;
using SocialMedia.Domain;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [CustomExceptionFilter]
    public class BaseController : Controller
    {
        /*private readonly IUserRepository _userRepository;
        public BaseController( IUserRepository repository )
        {
            _userRepository = repository;
        }

        public async Task<User?> GetCurrentUser()
        {
            return await _userRepository.FindByEmail(HttpContext.User.Identity!.Name!);
        }*/
    }
}
