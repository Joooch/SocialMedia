using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Models;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;
using System.Linq.Dynamic.Core;
using System.Text;

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
                offset = await source.MaxAsync(c => (DateTime?)c.CreatedAt);
            }

            // fetch some info about the query
            var total = enumerate ? await source.CountAsync() : 0; // enumerate if needed. it makes no sense to count items for infinite scroll

            // project to DTO
            var projectionResult = mapper.ProjectTo<TDto>(source);

            // filters
            projectionResult = projectionResult.ApplyFilters(pagedRequest);

            // sort
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

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> source, PagedRequest pagedRequest)
        {
            // TODO: Catch exceptions
            var requestFilters = pagedRequest.Filters;
            if (requestFilters == null || requestFilters.Length == 0)
            {
                return source;
            }

            var predicate = new StringBuilder();
            for (int i = 0; i < requestFilters.Length; i++)
            {
                if (i > 0)
                {
                    predicate.Append($" AND ");
                }
                predicate.Append(requestFilters[i].Path + $".{nameof(string.Contains)}(@{i})");
            }

            var propertyValues = requestFilters.Select(filter => filter.Value).ToArray();

            try
            {
                source = source.Where(predicate.ToString(), propertyValues);
            }
            catch (Exception ex)
            {
                // invalid filter, ignore.
            }

            return source;
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, PagedRequest pagedRequest)
        {
            // TODO: Catch exceptions
            if (pagedRequest.SortKey == null)
            {
                return source;
            }

            return source.OrderBy(pagedRequest.SortKey + " " + pagedRequest.SortDirection);
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
