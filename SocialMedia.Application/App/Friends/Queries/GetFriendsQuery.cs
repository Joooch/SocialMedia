using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Profiles.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;

namespace SocialMedia.Application.App.Friends.Queries
{
    public class GetFriendsQuery : IRequest<PaginatedResult<ProfileProtectedDto>>
    {
        public Guid UserId { get; set; }
        public PagedRequest PageInfo { get; set; }
    }

    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, PaginatedResult<ProfileProtectedDto>>
    {
        private readonly IFriendsRepository _friendsRepository;

        public GetFriendsQueryHandler(IFriendsRepository friendsRepository)
        {
            _friendsRepository = friendsRepository;
        }

        public async Task<PaginatedResult<ProfileProtectedDto>> Handle(GetFriendsQuery command, CancellationToken cancellationToken)
        {
            return await _friendsRepository.GetFriendsByUserId(command.UserId, command.PageInfo);
        }
    }
}
