using MediatR;
using SocialMedia.Application.Common.Interfaces.Repository;

namespace SocialMedia.Application.App.Friends.Commands
{
    public class SendFriendRequestCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid TargetId { get; set; }
    }

    public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand>
    {
        private readonly IFriendsRepository _friendsRepository;

        public SendFriendRequestCommandHandler(IFriendsRepository friendsRepository)
        {
            _friendsRepository = friendsRepository;
        }

        public async Task<Unit> Handle(SendFriendRequestCommand command, CancellationToken cancellationToken)
        {
            await _friendsRepository.SendFriendRequest(command.UserId, command.TargetId);
            return Unit.Value;
        }
    }
}
