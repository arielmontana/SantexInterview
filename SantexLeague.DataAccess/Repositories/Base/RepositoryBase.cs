using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SantexLeague.Common;
using SantexLeague.DataAccess.EntityFramework;

namespace SantexLeague.DataAccess.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity<int>
    {
        protected readonly SantexContext dbContext;

        public RepositoryBase(SantexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return dbContext.Set<T>().AsEnumerable<T>();
            });
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }
    }
}