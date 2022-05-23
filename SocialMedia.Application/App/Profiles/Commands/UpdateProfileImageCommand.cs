using MediatR;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;

namespace SocialMedia.Application.App.Profiles.Commands
{
    public class UpdateProfileImageCommand : IRequest<ProfileImageUpdateDto>
    {
        public IFormFile FileForm { get; set; }
        public Guid UserId { get; set; }
        public string RootPath { get; set; }
    }

    public class UpdateProfileImageCommandHandler : IRequestHandler<UpdateProfileImageCommand, ProfileImageUpdateDto>
    {
        private readonly IUserRepository _userRepository;

        public UpdateProfileImageCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ProfileImageUpdateDto> Handle(UpdateProfileImageCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var user = await _userRepository.GetById(request.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var publicPath = "img/users/" + userId + ".webp";
            var staticFolderPath = Path.Combine(request.RootPath, "Static");
            var fullPath = Path.Combine(staticFolderPath, publicPath);


            IImageFormat format;
            using var fileStream = request.FileForm.OpenReadStream();
            using var image = Image.Load(fileStream, out format);


            image.Mutate(c => c.Resize(new ResizeOptions
            {
                Size = new Size(128, 128),
                Mode = ResizeMode.Crop,
                Position = AnchorPositionMode.Center
            }));
            await image.SaveAsWebpAsync(fullPath);


            return new ProfileImageUpdateDto()
            {
                FilePath = publicPath
            };
        }
    }
}
