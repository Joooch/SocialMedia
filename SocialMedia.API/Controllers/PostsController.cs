using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.Posts.Commands;
using SocialMedia.Application.App.Posts.Queries;
using SocialMedia.Application.App.Posts.Responses;
using SocialMedia.Application.Common.Models;
using System.ComponentModel;

namespace SocialMedia.API.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IWebHostEnvironment _appEnv;
        private readonly IMediator _mediator;

        public PostsController(IWebHostEnvironment appEnv, IMediator mediator)
        {
            _appEnv = appEnv;
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreatePostRequest request)
        {
            var post = await _mediator.Send(new CreatePostCommand()
            {
                Request = request,
                UserId = HttpContext.User.GetUserId()
            });
            return Ok(post);
        }


        [HttpPost("GetFeed")]
        public async Task<PaginatedResult<PostDto>> GetFeed(PagedRequest page)
        {
            var posts = await _mediator.Send(new GetFeedQuery()
            {
                UserId = HttpContext.User.GetUserId(),
                PageInfo = page
            });

            return posts;
        }

        [HttpPost("UploadImage")]
        public async Task<UploadedImageDto> UploadImage(IFormFile file)
        {
            var uploadedImageDto = await _mediator.Send(new UploadImageCommand()
            {
                FileForm = file,
                RootPath = _appEnv.ContentRootPath,
                UserId = HttpContext.User.GetUserId()
            });
            return uploadedImageDto;
        }

        // Likes
        [HttpGet("{postId}/Like")]
        public async Task<ActionResult> GetLikes(string postId)
        {
            var likes = await _mediator.Send(new GetLatestLikesQuery()
            {
                UserId = HttpContext.User.GetUserId(),
                PostId = Guid.Parse(postId)
            });

            return Ok(likes);
        }

        [HttpPost("{postId}/Like")]
        public async Task<ActionResult> Like(string postId)
        {
            var like = await _mediator.Send(new AddLikeCommand()
            {
                UserId = HttpContext.User.GetUserId(),
                PostId = Guid.Parse(postId)
            });

            return Ok(like);
        }

        [HttpDelete("{postId}/Like")]
        public async Task<ActionResult> UnLike(string postId)
        {
            var like = await _mediator.Send(new RemoveLikeCommand()
            {
                UserId = HttpContext.User.GetUserId(),
                PostId = Guid.Parse(postId)
            });

            return Ok(like);
        }
    }
}
