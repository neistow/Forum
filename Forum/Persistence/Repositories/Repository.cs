using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using Antlr.Runtime.Misc;
using Forum.Core.Repositories;

namespace Forum.Persistence.Repositories
{
    public class Repository <TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public TEntity Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new System.NotImplementedException();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            throw new System.NotImplementedException();
        }

        public void Add(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}