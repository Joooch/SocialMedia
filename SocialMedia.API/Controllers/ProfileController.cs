using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Exceptions;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.Profiles.Commands;
using SocialMedia.Application.App.Profiles.Queries;
using SocialMedia.Application.App.Profiles.Responses;

namespace SocialMedia.API.Controllers
{
    public class ProfileController : BaseController
    {
        private IWebHostEnvironment _appEnv;
        private readonly IMediator _mediator;

        public ProfileController(IWebHostEnvironment appEnv, IMediator mediator)
        {
            _appEnv = appEnv;
            _mediator = mediator;
        }

        [HttpPut("")]
        public async Task<ProfileDto> Update(UpdateProfileRequest updateProfileRequest)
        {
            var profileDto = await _mediator.Send(new UpdateProfileCommand()
            {
                Request = updateProfileRequest,
                UserId = HttpContext.User.GetUserId(),
            });
            return profileDto;
        }

        [HttpPut("image")]
        public async Task<BookSuccessImageUpdateDto> UpdateImage(IFormFile file)
        {
            var userId = HttpContext.User.GetUserId();
            var rootPath = _appEnv.ContentRootPath;
            var responseDto = await _mediator.Send(new UpdateProfileImageCommand()
            {
                UserId = userId,
                FileForm = file,
                RootPath = rootPath
            });

            return responseDto;
        }

        [HttpGet("{userId}")]
        public async Task<ProfileProtectedDto> GetById(string userId)
        {
            var profileDto = await _mediator.Send(new GetProfileByUserIdQuery() { UserId = userId });
            if (profileDto is null)
            {
                throw new InvalidUserException(userId);
            }

            return profileDto;
        }
    }
}
