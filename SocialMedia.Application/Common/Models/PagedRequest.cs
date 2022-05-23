namespace SocialMedia.Application.Common.Models
{
    public class PagedRequest
    {
        public Guid? LastId { get; set; }
        public int PageSize { get; set; }
    }
}
