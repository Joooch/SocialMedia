
using SocialMedia.Domain;
using SocialMedia.Common.Dtos.User;

namespace SocialMedia.API.Middleware
{
    public class AutoMapperConfiguration : AutoMapper.Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Profile, ProfileDto>().ReverseMap();
            CreateMap<Profile, ProfileProtectedDto>();
            CreateMap<ProfileUpdateDto, Profile>();
        }
    }
}
