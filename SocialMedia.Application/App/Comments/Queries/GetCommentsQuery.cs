using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.App.Comments.Queries
{
    public class GetCommentsQuery : IRequest<PaginatedResult<CommentDto>>
    {
        public Guid UserId { get; set; }
        public PagedRequest? PageInfo { get; set; }
    }

    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, PaginatedResult<CommentDto>>
    {
        private readonly ICommentRepository _commentsRepository;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(ICommentRepository commentsRepository, IMapper mapper)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var page = request.PageInfo ?? new PagedRequest() { PageSize = 10 };
            var comments = await _commentsRepository.GetCommentsByUserId(request.UserId, page);

            return comments;
        }
    }
}
