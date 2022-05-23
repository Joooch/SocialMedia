using SocialMedia.Domain;

namespace SocialMedia.Application.Common.Interfaces.Repository
{
    public interface IBaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        
    }
}