using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntityFrameworkCore.DataContract;
using EntityFrameworkCore.DBContextManager.Config;
using EntityFrameworkCore.DBContextManager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SantexLeague.Common;

namespace EntityFrameworkCore.DBContextManager.Base
{
    /// <summary>
    /// Base class for entityframework for business class
    /// </summary>
    /// <typeparam name="TEntity">Type of business class</typeparam>
    /// <typeparam name="TDbContext">Context</typeparam>
    public class BaseRepository<TEntity, TDbContext, TEntityId> : Repository<TDbContext>, IDBRepository<TEntity, TEntityId> where TEntity : class, IEntity<TEntityId> where TDbContext : DbContext
    {
        public BaseRepository(IAmbientDbContextLocator ambientDbContextLocator, IDbContextScopeFactory dbContextScopeFactory, IOptions<DataBaseConfiguration> options) : base(ambientDbContextLocator, dbContextScopeFactory, options)
        {
        }

        public bool isPendingCommit { get; set; } = false;

        /// <summary>
        /// Save changes in Context.
        /// </summary>
        public virtual void SaveChanges()
        {
            AmbientDbContext.SaveChanges();
        }

        /// <summary>
        /// Create a context. In a context you can edit results of any EF query and Save Changes.
        /// You must use it with a using statement. it's only neccesary when you update data.
        /// it creates a new connection of a DBContext inside of a disposable object (RepoScope).
        /// </summary>
        /// <returns></returns>
        public virtual ScopesRepository CreateUpdateScope()
        {
            isPendingCommit = true;
            var repoContext = new ScopesRepository(_dbContextScopeFactory.Create());
            repoContext.OnDispose += DisposeContext;
            return repoContext;
        }

        /// <summary>
        /// Delegate that disposes the context when you really need create it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisposeContext(object sender, EventArgs e)
        {
            isPendingCommit = false;
        }

        /// <summary>
        /// Get one entity by id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <returns>One entity</returns>
        public virtual TEntity GetById(TEntityId id, string includes = "")
        {
            if (isPendingCommit)
                return GetByIdPendingCommit(id, includes);

            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return GetByIdPendingCommit(id, includes);
                }
            }
        }

        /// <summary>
        /// Get entities by Id range
        /// </summary>
        /// <param name="ids">The range of ids</param>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <returns></returns>
        public ICollection<TEntity> GetByRangeId(IEnumerable<TEntityId> ids, string includes = "")
        {
            if (isPendingCommit)
                return GetByIdPendingCommit(ids, includes);

            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return GetByIdPendingCommit(ids, includes);
                }
            }
        }

        /// <summary>
        /// Get entities by filter
        /// </summary>
        /// <param name="query">Filters</param>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <param name="order">Order by</param>
        /// <param name="orderAsc">True if it´s ascendent or false if it´s decendent</param>
        /// <param name="page">From what page</param>
        /// <param name="pageSize">Limit of entities to retrive</param>
        /// <returns>A list of entities</returns>
        public ICollection<TEntity> GetByFilter(Expression<Func<TEntity, bool>> query, string includes = "", Expression<Func<TEntity, object>> order = null, bool orderAsc = true, int? page = default(int?), int? pageSize = default(int?))
        {
            if (isPendingCommit)
                return GetByFilterPendingCommit(query, includes, order, orderAsc, page, pageSize);

            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return GetByFilterPendingCommit(query, includes, order, orderAsc, page, pageSize);
                }
            }
        }

        /// <summary>
        /// Get count of entities by filter
        /// </summary>
        /// <param name="query">Filter</param>
        /// <returns>A list of entities</returns>
        public long Count(Expression<Func<TEntity, bool>> query)
        {
            if (isPendingCommit)
                return CountPendingCommit(query);

            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return CountPendingCommit(query);
                }
            }
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <returns>A list of entities</returns>
        public ICollection<TEntity> GetAll(string includes = "")
        {
            if (isPendingCommit)
                return GetAllPendingCommit(includes);

            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return GetAllPendingCommit(includes);
                }
            }
        }

        /// <summary>
        /// Mark one entity as deleted
        /// </summary>
        /// <param name="id">Id of entity</param>
        public void MarkAsDeleted(TEntityId id)
        {
            var entityToBeDeleted = AmbientDbContext.Find<TEntity>(id);
            AmbientDbContext.Remove(entityToBeDeleted);
        }

        /// <summary>
        /// Mark one entity as deleted
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void MarkAsDeleted(TEntity entity)
        {
            AmbientDbContext.Remove(entity);
        }

        /// <summary>
        /// Get max by filter
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="func">Entity´s field selector</param>
        /// <param name="wherePredicate">filter</param>
        /// <returns>A max value</returns>
        public TResult GetMax<TResult>(Expression<Func<TEntity, TResult>> func, Expression<Func<TEntity, bool>> wherePredicate)
        {
            if (isPendingCommit)
                return GetMaxPendingCommit(func, wherePredicate);

            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return GetMaxPendingCommit(func, wherePredicate);
                }
            }
        }

        /// <summary>
        /// Get min by filter
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="func">Entity´s field selector</param>
        /// <param name="wherePredicate">filter</param>
        /// <returns>A min value</returns>
        public TResult GetMin<TResult>(Expression<Func<TEntity, TResult>> func, Expression<Func<TEntity, bool>> wherePredicate)
        {
            if (isPendingCommit)
                return GetMinPendingCommit(func, wherePredicate);

            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return GetMinPendingCommit(func, wherePredicate);
                }
            }
        }

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void Add(TEntity entity)
        {
            AmbientDbContext.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Add range of entities
        /// </summary>
        /// <param name="entities">Range of Entities to add</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            AmbientDbContext.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Attach entity to start tracking
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        public void Update(TEntity entity)
        {
            AmbientDbContext.Set<TEntity>().Attach(entity).Collections.All(x => x.IsModified);
            AmbientDbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Update range of entities
        /// </summary>
        /// <param name="entities">Range of Entities to update</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(x => Update(x));
        }

        #region [private methods]

        /// <summary>
        /// Get one entity by id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <returns>One entity</returns>
        public virtual TEntity GetByIdPendingCommit(TEntityId id, string includes = "")
        {
            var dbSet = AmbientDbContext.Set<TEntity>();
            var queryable = dbSet.AsQueryable();
            if (!string.IsNullOrEmpty(includes))
            {
                var arrIcludes = includes.Split(',');
                foreach (var i in arrIcludes)
                {
                    queryable = queryable.Include(i.Trim());
                }
            }
            var result = queryable.FirstOrDefault(x => x.Id.Equals(id));
            return result;
        }

        /// <summary>
        /// Get entities by id
        /// </summary>
        /// <param name="ids">ids</param>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <returns>A IEnumerable<TEntity></returns>
        public virtual ICollection<TEntity> GetByIdPendingCommit(IEnumerable<TEntityId> ids, string includes = "")
        {
            var dbSet = AmbientDbContext.Set<TEntity>();
            var queryable = dbSet.AsQueryable();
            if (!string.IsNullOrEmpty(includes))
            {
                var arrIcludes = includes.Split(',');
                foreach (var i in arrIcludes)
                {
                    queryable = queryable.Include(i.Trim());
                }
            }
            var result = queryable.Where(x => ids.Contains(x.Id)).ToList();
            return result;
        }

        /// <summary>
        /// Get entities by filter
        /// </summary>
        /// <param name="query">Filters</param>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <param name="order">Order by</param>
        /// <param name="orderAsc">True if it´s ascendent or false if it´s decendent</param>
        /// <param name="page">From what page</param>
        /// <param name="pageSize">Limit of entities to retrive</param>
        /// <returns>A list of entities</returns>
        private ICollection<TEntity> GetByFilterPendingCommit(Expression<Func<TEntity, bool>> query, string includes = "", Expression<Func<TEntity, object>> order = null, bool orderAsc = true, int? page = default(int?), int? pageSize = default(int?))
        {
            var dbSet = AmbientDbContext.Set<TEntity>();
            var queryable = dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(includes))
            {
                var arrIcludes = includes.Split(',');
                foreach (var i in arrIcludes)
                {
                    queryable = queryable.Include(i.Trim());
                }
            }

            var result = queryable.Where(query);

            if (order != null)
                if (orderAsc)
                    result = result.OrderBy(order);
                else
                    result = result.OrderByDescending(order);

            if (page != null && pageSize != null)
            {
                //TODO: There is a bug related with the take sentece for mysql https://bugs.mysql.com/bug.php?id=82991
                int skip = (page.Value - 1) * pageSize.Value;
                int take = pageSize.Value;
                //result = result.Skip(skip).Take(take);
            }

            return result.ToList();
        }

        /// <summary>
        /// Get count of entities by filter
        /// </summary>
        /// <param name="query">Filter</param>
        /// <returns>A list of entities</returns>
        private long CountPendingCommit(Expression<Func<TEntity, bool>> query)
        {
            var entity = AmbientDbContext.Set<TEntity>();
            return entity.LongCount(query);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="includes">Specify with navigation should be includes (comma separated)</param>
        /// <returns>A list of entities</returns>
        private ICollection<TEntity> GetAllPendingCommit(string includes = "")
        {
            var dbSet = AmbientDbContext.Set<TEntity>();
            var queryable = dbSet.AsQueryable();
            if (!string.IsNullOrEmpty(includes))
            {
                var arrIcludes = includes.Split(',');
                foreach (var i in arrIcludes)
                {
                    queryable = queryable.Include(i.Trim());
                }
            }
            var result = queryable.ToList();
            return result;
        }

        /// <summary>
        /// Get max by filter
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="func">Entity´s field selector</param>
        /// <param name="wherePredicate">filter</param>
        /// <returns>A max value</returns>
        private TResult GetMaxPendingCommit<TResult>(Expression<Func<TEntity, TResult>> func, Expression<Func<TEntity, bool>> wherePredicate)
        {
            return AmbientDbContext.Set<TEntity>().Where(wherePredicate).Max(func);
        }

        /// <summary>
        /// Get min by filter
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="func">Entity´s field selector</param>
        /// <param name="wherePredicate">filter</param>
        /// <returns>A min value</returns>
        private TResult GetMinPendingCommit<TResult>(Expression<Func<TEntity, TResult>> func, Expression<Func<TEntity, bool>> wherePredicate)
        {
            return AmbientDbContext.Set<TEntity>().Where(wherePredicate).Min(func);
        }

        #endregion [private methods]
    }
}