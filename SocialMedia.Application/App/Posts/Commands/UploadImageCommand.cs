using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SocialMedia.Application.App.Posts.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain;

namespace SocialMedia.Application.App.Posts.Commands
{
    public class UploadImageCommand : IRequest<UploadedImageDto>
    {
        public Guid UserId { get; set; }
        public IFormFile FileForm { get; set; }
        public string RootPath { get; set; }
    }

    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadedImageDto>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IUserRepository _userRepository;

        public UploadImageCommandHandler(IUserRepository userRepository, IImageRepository imageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _userRepository = userRepository;
        }

        public async Task<UploadedImageDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var imageEntity = _imageRepository.Add(new ImageEntity()
            {
                OwnerId = userId,
            });

            var publicPath = "img/posts/" + imageEntity.Entity.Id + ".webp";
            var staticFolderPath = Path.Combine(request.RootPath, "Static");
            var fullPath = Path.Combine(staticFolderPath, publicPath);


            IImageFormat format;
            using var fileStream = request.FileForm.OpenReadStream();
            using var image = Image.Load(fileStream, out format);


            image.Mutate(c => c.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(1920, 1080)
            }));
            await image.SaveAsWebpAsync(fullPath);


            await _imageRepository.SaveAsync();
            
            return _mapper.Map<ImageEntity, UploadedImageDto>(imageEntity.Entity);
        }
    }
}
