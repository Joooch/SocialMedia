using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Posts.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.App.Posts.Queries
{
    public class GetFeedQuery : IRequest<IList<PostDto>>
    {
        public Guid UserId { get; set; }
        public PagedRequest? PageInfo { get; set; }
    }

    public class GetFeedQueryHandler : IRequestHandler<GetFeedQuery, IList<PostDto>>
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;

        public GetFeedQueryHandler(IPostsRepository postsRepository, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
        }

        public async Task<IList<PostDto>> Handle(GetFeedQuery request, CancellationToken cancellationToken)
        {
            /*var posts = await _postsRepository.GetPostsByUserId(request.UserId, new PagedRequest()
            {
                PageSize = 10
            });*/
            var page = request.PageInfo ?? new PagedRequest() { PageSize = 10 };
            var posts = await _postsRepository.GetPostsByUserId(request.UserId, page);

            return _mapper.Map<IList<PostDto>>(posts);
        }
    }
}
