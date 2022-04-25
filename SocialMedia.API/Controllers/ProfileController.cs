using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common.Dtos.User;

namespace SocialMedia.API.Controllers
{
    public class ProfileController : BaseController
    {
        [HttpPut("")]
        public IActionResult Update( ProfileDto profile )
        {
            return View();
        }
    }
}
