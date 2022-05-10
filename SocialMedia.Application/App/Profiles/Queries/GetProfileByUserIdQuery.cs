using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Application.App.Profiles.Queries
{
    public class GetProfileByUserIdQuery : IRequest<ProfileProtectedDto?>
    {
        public string UserId { get; set; }
    }

    public class GetProfileByUserIdQueryyHandler : IRequestHandler<GetProfileByUserIdQuery, ProfileProtectedDto?>
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;

        public GetProfileByUserIdQueryyHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        public async Task<ProfileProtectedDto?> Handle(GetProfileByUserIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await _profileRepository.GetByUserId(request.UserId);
            if (profile is null)
            {
                return null;
            }

            var profileDto = _mapper.Map<ProfileProtectedDto>(profile);
            return profileDto;
        }
    }
}
