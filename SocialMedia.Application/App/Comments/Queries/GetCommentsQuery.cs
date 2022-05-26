using MediatR;
using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.App.Comments.Queries
{
    public class GetCommentsQuery : IRequest<PaginatedResult<CommentDto>>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public PagedRequest? PageInfo { get; set; }
    }

    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, PaginatedResult<CommentDto>>
    {
        private readonly ICommentRepository _commentsRepository;

        public GetCommentsQueryHandler(ICommentRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<PaginatedResult<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var page = request.PageInfo ?? new PagedRequest() { PageSize = 10 };
            var comments = await _commentsRepository.GetCommentsByUserId(request.UserId, request.PostId, page);

            return comments;
        }
    }
}
