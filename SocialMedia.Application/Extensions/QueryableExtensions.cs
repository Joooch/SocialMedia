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
        public static async Task<PaginatedResult<TDto>> ApplyPaginatedResultAsync<TEntity, TDto>(this IQueryable<TEntity> source, PagedRequest pagedRequest, IMapper mapper) where TEntity : BaseEntity, ITimedEntity
        {
            // apply offset
            source = source.ApplyOffset(pagedRequest);

            // fetch some usefull info
            var total = await source.CountAsync();
            var offset = pagedRequest.Offset ?? (await source.MaxAsync(c => c.CreatedAt));

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
            return source.OrderBy("CreatedAt descending");
        }

        public static IQueryable<T> ApplyOffset<T>(this IQueryable<T> source, PagedRequest pagedRequest) where T : BaseEntity, ITimedEntity
        {
            if (pagedRequest.Offset.HasValue)
            {
                return source.Where(c => c.CreatedAt < pagedRequest.Offset.Value);
            }

            return source;
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, PagedRequest pagedRequest)
        {
            return source
                .Skip(pagedRequest.Page * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize);
        }
    }
}
