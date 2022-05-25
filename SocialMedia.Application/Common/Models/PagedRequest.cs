using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Application.Common.Models
{
    public class PagedRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public DateTime? Offset { get; set; }

        public string? SortKey { get; set; }
        public string SortDirection { get; set; } = "ASC"; // DESC

        public Filter[]? Filters { get; set; }
    }
}
