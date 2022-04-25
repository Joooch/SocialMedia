﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SocialMedia.Common.Dtos.User;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Domain;

namespace SocialMedia.API.Controllers
{
    public class ProfileController : BaseController
    {
        private IProfileRepository _profileRepository { get; set; }
        private IUserRepository _userRepository { get; set; }
        private IWebHostEnvironment _appEnv;
        private readonly IMapper _mapper;


        public ProfileController(IWebHostEnvironment appEnv, IUserRepository userRepository, IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _appEnv = appEnv;
            _mapper = mapper;
        }

        [HttpPut("")]
        public async Task<ProfileDto> Update(ProfileUpdateDto profileDto)
        {
            var user = await _userRepository.GetByEmail(HttpContext.User.Identity!.Name!);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var profile = await _profileRepository.GetByUser(user);
            if (profile == null)
            {
                profile = _mapper.Map<Domain.Profile>(profileDto);
                profile.User = user;
                _profileRepository.Add(profile);
                await _profileRepository.SaveAsync();
            }
            else
            {
                profile.FirstName = profileDto.FirstName;
                profile.LastName = profileDto.LastName;
                profile.Address = profileDto.Address;
                profile.City = profileDto.City;
                profile.Region = profileDto.Region;
                profile.Country = profileDto.Country;

                await _profileRepository.SaveAsync();
            }

            //var profile = _mapper.Map<Profile>(profileDto);
            //_profileRepository.Remove()
            //_profileRepository.Add()
            return _mapper.Map<ProfileDto>(profile);
        }

        [HttpPut("image")]
        public async Task<IActionResult> UpdateImage(IFormFile file)
        {
            var user = await _userRepository.GetByEmail(HttpContext.User.Identity!.Name!);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            //var filename = Path.Combine(builder.Environment.ContentRootPath, "Static/img");
            var publicPath = "img/users/" + user.UserId + ".webp";
            var staticFolderPath = Path.Combine(_appEnv.ContentRootPath, "Static");
            var fullPath = Path.Combine(staticFolderPath, publicPath);

            IImageFormat format;
            using var fileStream = file.OpenReadStream();
            using var image = Image.Load(fileStream, out format);

            image.Mutate(c => c.Resize(new ResizeOptions
            {
                Size = new Size(128, 128),
                Mode = ResizeMode.Crop,
                Position = AnchorPositionMode.Center
            }));
            image.SaveAsWebp(fullPath);

            return Ok(new { filename = "/" + publicPath });
        }
    }
}
