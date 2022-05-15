using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Domain;
using SocialMedia.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Application.App.Profiles.Commands
{
    public class UpdateProfileRequest
    {
        [Required, MinLength(3), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string LastName { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string Address { get; set; }

        [Required, MinLength(5), MaxLength(30)]
        public string City { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string Region { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        public string Country { get; set; }
    }

    public class UpdateProfileCommand : IRequest<ProfileDto>
    {
        public Guid UserId { get; set; }
        public UpdateProfileRequest Request { get; set; }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;

        public UpdateProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }

        public async Task<ProfileDto> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(command.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            var request = command.Request;

            var profile = await _profileRepository.GetByUser(user);
            if (profile == null)
            {
                profile = _mapper.Map<ProfileEntity>(request);
                profile.User = user;

                _profileRepository.Add(profile);
                await _profileRepository.SaveAsync();
            }
            else
            {
                profile.FirstName = request.FirstName;
                profile.LastName = request.LastName;
                profile.Address = request.Address;
                profile.City = request.City;
                profile.Region = request.Region;
                profile.Country = request.Country;

                await _profileRepository.SaveAsync();
            }

            return _mapper.Map<ProfileDto>(profile);
        }
    }
}
