using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Foundation.DatabaseAccessor
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;
        private readonly DbContext _dbContext;

        public Repository(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetService(InternalDbContext.MainDbContextType) as DbContext;
            _dbContext = dbContext;
            _entities = dbContext!.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> AsQueryable() => _entities.AsQueryable();
    }

    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        IQueryable<TEntity> AsQueryable();
    }
}
