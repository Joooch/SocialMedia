using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.App.Users.Responses;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Application.App.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository, IProfileRepository profileRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var profile = await _profileRepository.GetByUser(user);
            var userDto = new UserDto()
            {
                Profile = profile != null ? _mapper.Map<ProfileDto>(profile) : null,
                Email = user.Email,
            };

            return userDto;
        }
    }
}
