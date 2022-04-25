using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SocialMedia.Common.Dtos.User;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.API.Controllers
{
    public class ProfileController : BaseController
    {
        private IUserRepository _userRepository { get; set; }
        private IWebHostEnvironment _appEnv;


        public ProfileController(IWebHostEnvironment appEnv, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _appEnv = appEnv;
        }

        [HttpPut("")]
        public IActionResult Update(ProfileDto profile)
        {
            return Ok();
        }

        [HttpPut("image")]
        public async Task<IActionResult> UpdateImage(IFormFile file)
        {
            var user = await _userRepository.GetByEmail(HttpContext.User.Identity!.Name!);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            //var filename = Path.Combine(builder.Environment.ContentRootPath, "Static/img");
            var publicPath = "img/users/" + user.UserId + ".webp";
            var staticFolderPath = Path.Combine(_appEnv.ContentRootPath, "Static");
            var fullPath = Path.Combine(staticFolderPath, publicPath);

            IImageFormat format;
            using var fileStream = file.OpenReadStream();
            using var image = Image.Load(fileStream, out format);

            image.Mutate(c => c.Resize(new ResizeOptions
            {
                Size = new Size(128, 128),
                Mode = ResizeMode.Crop,
                Position = AnchorPositionMode.Center
            }));
            image.SaveAsWebp(fullPath);

            return Ok(new { filename = "/" + publicPath });
        }
    }
}
