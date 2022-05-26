using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.Comments.Commands;
using SocialMedia.Application.App.Comments.Queries;
using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.API.Controllers
{
    public class CommentController : BaseController
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateComment")]
        public async Task<CommentDto> CreateComment(CreateCommentRequest comment)
        {
            var result = await _mediator.Send(new CreateCommentCommand()
            {
                Request = comment,
                UserId = HttpContext.User.GetUserId()
            });
            return result;
        }

        [HttpPost("FindInPost/{postId}")]
        public async Task<PaginatedResult<CommentDto>> GetFeed(string postId, [FromBody] PagedRequest page)
        {
            var posts = await _mediator.Send(new GetCommentsQuery()
            {
                UserId = HttpContext.User.GetUserId(),
                PageInfo = page,
                PostId = Guid.Parse(postId)
            });

            return posts;
        }
    }
}
