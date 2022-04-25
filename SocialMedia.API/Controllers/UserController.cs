using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common.Dtos.User;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IProfileRepository profileRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }

        [HttpGet()]
        [Authorize]
        public async Task<UserDto> Get()
        {
            var user = await _userRepository.GetByEmail(HttpContext.User.Identity!.Name!);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var profile = await _profileRepository.GetByUser(user);
            var response = new UserDto()
            {
                Profile = profile != null ? _mapper.Map<ProfileDto>(profile) : null,
                Email = user.Email,
            };

            return response;
        }
    }
}
