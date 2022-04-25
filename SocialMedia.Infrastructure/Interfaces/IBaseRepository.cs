using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        DbSet<T> EntitySet { get; }
    }
}