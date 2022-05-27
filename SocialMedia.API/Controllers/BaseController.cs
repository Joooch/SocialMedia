using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Exceptions;
using SocialMedia.API.Extensions;

namespace SocialMedia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaseController : Controller
    {
        protected Guid GetUserId()
        {
            return HttpContext.User.GetUserId();
        }
    }
}
