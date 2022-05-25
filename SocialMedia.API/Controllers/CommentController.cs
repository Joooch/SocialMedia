using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.Comments.Commands;
using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;

namespace SocialMedia.API.Controllers
{
    public class CommentController : BaseController
    {
        private readonly IMediator _mediator;

        public CommentController( IMediator mediator )
        {
            _mediator = mediator;
        }

        [HttpPost("createComment")]
        public async Task<CommentDto> CreateComment(CreateCommentRequest comment)
        {
            var result = await _mediator.Send(new CreateCommentCommand() {
                Request = comment,
                UserId = HttpContext.User.GetUserId()
            });
            return result;
        }
    }
}
