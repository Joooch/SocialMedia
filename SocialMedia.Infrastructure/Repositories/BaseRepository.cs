using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;
using System.Linq.Expressions;

namespace SocialMedia.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _dbContext;
        public BaseRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public DbSet<T> EntitySet
        {
            get => _dbContext.Set<T>();
        }

        public EntityEntry<T> Add(T entity)
        {
            return EntitySet.Add(entity);
        }

        public EntityEntry<T> Remove(T entity)
        {
            return EntitySet.Remove(entity);
        }

        public Task<T?> Get(Expression<Func<T, bool>> filter)
        {
            return EntitySet.FirstOrDefaultAsync(filter);
        }

        public Task<List<T>> GetAll()
        {
            return EntitySet.ToListAsync();
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
