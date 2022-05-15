using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Domain;

namespace SocialMedia.Application.MapperProfiles
{
    public class ProfilesMapper : AutoMapper.Profile
    {
        public ProfilesMapper()
        {
            CreateMap<ProfileEntity, ProfileDto>().ReverseMap();
            CreateMap<ProfileEntity, ProfileProtectedDto>();
        }
    }
}
