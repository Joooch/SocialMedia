using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Application.Common.Models;
using SocialMedia.Application.Extensions;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
    {
        private readonly IMapper _mapper;

        public CommentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CommentDto>> GetCommentsByUserId(Guid userId, Guid postId, PagedRequest pagedRequest)
        {
            var query = EntitySet;

            return await query
                .Where(x => x.PostId == postId)
                .Include(c => c.Owner)
                .ApplyPaginatedResultAsync<CommentEntity, CommentDto>(pagedRequest, _mapper, true);
        }
    }
}
