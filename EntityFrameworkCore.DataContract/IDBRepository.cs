using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkCore.DataContract
{
    public interface IDBRepository<TEntity> : IDBRepository<TEntity, long> { }

    public interface IDBRepository<TEntity, TEntityId>
    {
        TEntity GetById(TEntityId id, string includes = "");
        ICollection<TEntity> GetByRangeId(IEnumerable<TEntityId> ids, string includes = "");
        ICollection<TEntity> GetByFilter(Expression<Func<TEntity, bool>> query, string include = "", Expression<Func<TEntity, Object>> order = null, bool orderAsc = true, int? page = null, int? pageSize = null);
        TResult GetMax<TResult>(Expression<Func<TEntity, TResult>> func, Expression<Func<TEntity, bool>> where);
        TResult GetMin<TResult>(Expression<Func<TEntity, TResult>> func, Expression<Func<TEntity, bool>> where);
        ICollection<TEntity> GetAll(string include = "");
        long Count(Expression<Func<TEntity, bool>> query);
        void MarkAsDeleted(TEntityId id);
        void MarkAsDeleted(TEntity entity);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void SaveChanges();
        ScopesRepository CreateUpdateScope();
    }
}