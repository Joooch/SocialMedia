using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialMedia.Domain.Entities;
using System.Linq.Expressions;

namespace SocialMedia.Application.Common.Interfaces.Repository
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
