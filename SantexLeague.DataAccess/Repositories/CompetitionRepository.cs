using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SantexLeague.DataAccess.EntityFramework;
using SantexLeague.DataAccess.Repositories.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.Repositories
{
    public class CompetitionRepository : RepositoryBase<Competition>, ICompetitionRepository
    {
        public CompetitionRepository(SantexContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Exist(string code)
        {
            return await dbContext.Competitions.AsNoTracking()
                .AnyAsync(c => string.Equals(c.Code, code, System.StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Competition> GetByCodeAsync(string code)
        {
            return await dbContext.Competitions.AsNoTracking()
                .FirstOrDefaultAsync(c => string.Equals(c.Code, code, System.StringComparison.OrdinalIgnoreCase));
        }


    }
}