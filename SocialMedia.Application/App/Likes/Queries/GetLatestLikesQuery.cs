using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Likes.Responses;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;

namespace SocialMedia.Application.App.Posts.Queries
{
    public class GetLatestLikesQuery : IRequest<LikesResponse>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }

    public class GetLatestLikesQueryHandler : IRequestHandler<GetLatestLikesQuery, LikesResponse>
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public GetLatestLikesQueryHandler(IProfileRepository profileRepository, ILikesRepository likesRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _likesRepository = likesRepository;
            _mapper = mapper;
        }

        public async Task<LikesResponse> Handle(GetLatestLikesQuery request, CancellationToken cancellationToken)
        {
            var ownerProfile = (await _profileRepository.GetByUserId(request.UserId))!;

            var totalCount = await _likesRepository.GetCountByPostId(request.PostId);
            var profiles = await _likesRepository.GetLatestByPostId(request.PostId, 3);
            var existingUserLike = await _likesRepository.GetLikeFromUser(ownerProfile.Id, request.PostId);

            return new LikesResponse()
            {
                TotalCount = totalCount,
                HasLike = existingUserLike is not null,
                OtherLikes = _mapper.Map<IList<ProfileProtectedDto>>(profiles)
            };
        }
    }
}
