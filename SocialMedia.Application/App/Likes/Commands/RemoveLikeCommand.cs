using AutoMapper;
using MediatR;
using SocialMedia.Application.Common.Interfaces.Repository;

namespace SocialMedia.Application.App.Posts.Commands
{
    public class RemoveLikeCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }

    public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILikesRepository _likeRepository;

        public RemoveLikeCommandHandler(IProfileRepository profileRepository, ILikesRepository likeRepository)
        {
            _profileRepository = profileRepository;
            _likeRepository = likeRepository;
        }

        public async Task<bool> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            var owner = (await _profileRepository.GetByUserId(request.UserId))!;
            var existingLike = await _likeRepository.GetLikeFromUser(owner.Id, request.PostId);

            if (existingLike is null)
            {
                return true;
            }

            _likeRepository.Remove(existingLike);
            await _likeRepository.SaveAsync();

            return true;
        }
    }
}
