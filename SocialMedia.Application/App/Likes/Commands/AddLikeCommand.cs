using AutoMapper;
using MediatR;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.App.Posts.Commands
{
    public class AddLikeCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }

    public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILikesRepository _likeRepository;

        public AddLikeCommandHandler(IProfileRepository profileRepository, ILikesRepository likeRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _likeRepository = likeRepository;
        }

        public async Task<bool> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            var owner = (await _profileRepository.GetByUserId(request.UserId))!;
            var existingLike = await _likeRepository.GetLikeFromUser(owner.Id, request.PostId);

            if (existingLike is not null)
            {
                return true;
            }

            var newLikeRecord = new LikeEntity();
            newLikeRecord.PostId = request.PostId;
            newLikeRecord.UserId = owner.Id;

            _likeRepository.Add(newLikeRecord);
            await _likeRepository.SaveAsync();

            return true;
        }
    }
}
