using MediatR;
using SocialMedia.Application.Common.Interfaces.Repository;

namespace SocialMedia.Application.App.Friends.Queries
{
    public class GetFriendsCountQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }

    public class GetFriendsCountQueryHandler : IRequestHandler<GetFriendsCountQuery, int>
    {
        private readonly IFriendsRepository _friendsRepository;

        public GetFriendsCountQueryHandler(IFriendsRepository friendsRepository)
        {
            _friendsRepository = friendsRepository;
        }

        public async Task<int> Handle(GetFriendsCountQuery command, CancellationToken cancellationToken)
        {
            return await _friendsRepository.GetFriendsCountByUserId(command.UserId);
        }
    }
}
