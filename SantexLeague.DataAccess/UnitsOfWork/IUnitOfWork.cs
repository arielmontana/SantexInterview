using System;
using System.Threading.Tasks;
using SantexLeague.DataAccess.EntityFramework;
using SantexLeague.DataAccess.Repositories;

namespace SantexLeague.DataAccess.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICompetitionRepository CompetitionRepository { get; }
        SantexContext Context { get; }
        Task CommitAsync();
    }
}