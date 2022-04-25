using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialMedia.Domain;
using System.Linq.Expressions;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T?> Get(Expression<Func<T, bool>> filter);
        public EntityEntry<T> Remove(T entity);

        public EntityEntry<T> Add(T entity);
        public Task<int> SaveAsync();
        public Task<List<T>> GetAll();
    }
}
