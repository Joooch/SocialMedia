using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Extensions;
using SocialMedia.Application.App.Posts.Commands;
using SocialMedia.Application.App.Posts.Queries;
using SocialMedia.Application.App.Posts.Responses;
using SocialMedia.Application.Common.Models;

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
    }
}
