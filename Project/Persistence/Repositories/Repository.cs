using Microsoft.EntityFrameworkCore;
using Project.Core.Repositories;
using System.Linq.Expressions;

namespace Project.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext Context;
        private DbSet<TEntity> _entity;

        public Repository(AppDbContext context)
        {
            Context = context;
            _entity = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entity.Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entity.AddRange(entities);
        }

        public TEntity Get(int id)
        {
            return _entity.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entity.ToList();
        }

        public IEnumerable<TEntity> GetAll(int pageIndex, int pageLength)
        {
            if (pageIndex <= 0 || pageLength <= 0)
                return GetAll();

            return _entity.Skip((pageIndex - 1) * pageLength).Take(pageLength).ToList();
        }

        public int Count()
        {
            return _entity.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Count(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate).ToList();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.SingleOrDefault(predicate);
        }

        public void Remove(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entity.RemoveRange(entities);
        }

    }
}
