using AutoMapper;
using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.App.Posts.Commands;
using SocialMedia.Application.App.Posts.Responses;
using SocialMedia.Application.App.Profiles.Commands;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.MapperProfiles
{
    public class ProfilesMapper : Profile
    {
        public ProfilesMapper()
        {
            CreateMap<ProfileEntity, ProfileDto>().ForMember(c => c.UserId, c => c.MapFrom(c => c.Id)).ReverseMap();
            CreateMap<ProfileEntity, ProfileProtectedDto>().ForMember(c => c.UserId, c => c.MapFrom(c => c.Id));

            CreateMap<UpdateProfileRequest, ProfileEntity>();
            CreateMap<CreatePostRequest, PostEntity>();

            CreateMap<PostEntity, PostDto>().ForMember(c => c.Images, c => c.MapFrom(c => c.Images.Select(i => i.Id)));
            CreateMap<CreatePostRequest, PostEntity>().ForMember(c => c.Images, c => c.Ignore());

            CreateMap<ImageEntity, UploadedImageDto>().ForMember(c => c.ImageId, c => c.MapFrom(c => c.Id));

            CreateMap<Guid, string>().ConvertUsing(c => c.ToString());

            CreateMap<CommentEntity, CommentDto>();
        }
    }
}
