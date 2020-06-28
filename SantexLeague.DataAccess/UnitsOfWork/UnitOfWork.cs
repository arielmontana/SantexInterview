using System.Threading.Tasks;
using SantexLeague.DataAccess.EntityFramework;
using SantexLeague.DataAccess.Repositories;

namespace SantexLeague.DataAccess.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public SantexContext Context { get; }

        public UnitOfWork(SantexContext context, ICompetitionRepository competitionRepository)
        {
            Context = context;
            CompetitionRepository = competitionRepository;
        }

        public ICompetitionRepository CompetitionRepository { get; }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}