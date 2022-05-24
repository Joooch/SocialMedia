using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public DateTime? Offset { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IList<T> Items { get; set; }
    }
}
