using SocialMedia.Domain;

namespace SocialMedia.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public int PageSize { get; set; }
        public IList<T> Items { get; set; }
    }
}
