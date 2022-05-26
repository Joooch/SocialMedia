using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.Friends.Commands;
using SocialMedia.Application.App.Friends.Queries;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.API.Controllers
{
    public class UserIdDto
    {
        public string UserId { get; set; }
    }

    public class FriendsController : BaseController
    {
        private readonly IMediator _mediator;

        public FriendsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sendRequest")]
        public async Task<IActionResult> SendFriendRequest(UserIdDto query)
        {
            await _mediator.Send(new SendFriendRequestCommand()
            {
                UserId = HttpContext.User.GetUserId(),
                TargetId = Guid.Parse(query.UserId),
            });
            return Ok();
        }

        [HttpPost("getFriendsCount")]
        public async Task<IActionResult> GetFriendsCount(GetFriendsCountQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(new
            {
                Count = response
            });
        }

        [HttpPost("getFriends")]
        public async Task<PaginatedResult<ProfileProtectedDto>> GetFriends(GetFriendsQuery query)
        {
            var response = await _mediator.Send(query);
            return response;
        }

        [HttpPost("getFriendStatus")]
        public async Task<FriendStatus> GetFriendStatus(UserIdDto query)
        {
            var response = await _mediator.Send(new GetFriendStatusQuery()
            {
                UserId = HttpContext.User.GetUserId(),
                FriendId = Guid.Parse(query.UserId)
            });

            return response;
        }
    }
}
