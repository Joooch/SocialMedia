using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.App.Posts.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;
using SocialMedia.Application.Extensions;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostsRepository : BaseRepository<PostEntity>, IPostsRepository
    {
        private readonly IMapper _mapper;

        public PostsRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedResult<PostDto>> GetPostsByUserId(Guid userId, PagedRequest pagedRequest)
        {
            var query = EntitySet;

            return await query
                .Include(c => c.UserOwner)
                .Include(c => c.Images)
                .ApplyPaginatedResultAsync<PostEntity, PostDto>(pagedRequest, _mapper);
        }
    }
}
