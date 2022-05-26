using MediatR;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.App.Friends.Queries
{
    public class GetFriendStatusQuery : IRequest<FriendStatus>
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
    }

    public class GetFriendStatusQueryHandler : IRequestHandler<GetFriendStatusQuery, FriendStatus>
    {
        private readonly IFriendsRepository _friendsRepository;

        public GetFriendStatusQueryHandler(IFriendsRepository friendsRepository)
        {
            _friendsRepository = friendsRepository;
        }

        public async Task<FriendStatus> Handle(GetFriendStatusQuery request, CancellationToken cancellationToken)
        {
            return await _friendsRepository.GetFriendStatus(request.UserId, request.FriendId);
        }
    }
}
