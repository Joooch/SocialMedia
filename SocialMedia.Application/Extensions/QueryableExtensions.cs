using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Models;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;
using System.Linq.Dynamic.Core;

namespace SocialMedia.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<TDto>> ApplyPaginatedResultAsync<TEntity, TDto>(this IQueryable<TEntity> source, PagedRequest pagedRequest, IMapper mapper, bool enumerate = false) where TEntity : BaseEntity, ITimedEntity
        {
            // apply offset
            DateTime? offset = pagedRequest.Offset;
            if (offset.HasValue)
            {
                source = source.ApplyOffset(offset.Value);
            }
            else
            {
                // Remember oldest date and save it as offset for next page request
                offset = await source.MaxAsync(c => c.CreatedAt);
            }

            // fetch some info about the query
            var total = enumerate ? await source.CountAsync() : 0; // enumerate if needed. it makes no sense to count items on infinite scroll

            // sort
            var projectionResult = mapper.ProjectTo<TDto>(source);
            projectionResult = projectionResult.ApplySort(pagedRequest);

            // do pagination after sort
            projectionResult = projectionResult.ApplyPagination(pagedRequest);

            // output result
            var resultList = await projectionResult.ToListAsync();
            return new PaginatedResult<TDto>()
            {
                Offset = offset,
                Page = pagedRequest.Page,
                PageSize = pagedRequest.PageSize,
                Items = resultList,
                Total = total
            };
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, PagedRequest pagedRequest)
        {
            return source.OrderBy("CreatedAt", "DESC"); // "ASC"
        }

        public static IQueryable<T> ApplyOffset<T>(this IQueryable<T> source, DateTime offset) where T : BaseEntity, ITimedEntity
        {
            return source.Where(c => c.CreatedAt <= offset);
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, PagedRequest pagedRequest)
        {
            return source
                .Skip(pagedRequest.Page * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize);
        }
    }
}
