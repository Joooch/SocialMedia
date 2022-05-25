using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Posts.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Application.App.Posts.Commands
{
    public class CreatePostRequest
    {
        [Required, MinLength(2), MaxLength(1024 * 4)]
        public string Content { get; set; }

        public string[]? Images { get; set; }
    }

    public class CreatePostCommand : IRequest<PostDto>
    {
        public Guid UserId { get; set; }
        public CreatePostRequest Request { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostsRepository _postRespository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IPostsRepository postsRepository, IProfileRepository profileRepository, IImageRepository imageRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _postRespository = postsRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var owner = (await _profileRepository.GetByUserId(request.UserId))!;

            var postEntity = _mapper.Map<PostEntity>(request.Request);
            postEntity.Owner = owner;
            postEntity.CreatedAt = DateTime.Now;

            var images = request.Request.Images;
            var imagesCount = images?.Length ?? 0;
            if (imagesCount > 0)
            {
                postEntity.Images = new List<ImageEntity>();
                foreach (var imageId in images!.Take(4))
                {
                    var image = await _imageRepository.GetById(Guid.Parse(imageId));
                    image.Post = postEntity;

                    postEntity.Images.Add(image);
                }
            }

            _postRespository.Add(postEntity);
            await _postRespository.SaveAsync();

            return _mapper.Map<PostDto>(postEntity);
        }
    }
}
