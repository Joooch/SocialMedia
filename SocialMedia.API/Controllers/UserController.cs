using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Exceptions;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.Users.Queries;
using SocialMedia.Application.App.Users.Responses;

namespace SocialMedia.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<UserDto> Get()
        {
            var userDto = await _mediator.Send(new GetUserByEmailQuery() { UserId = HttpContext.User.GetUserId( ) });
            return userDto;
        }
    }
}
