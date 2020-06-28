using System.Threading.Tasks;
using SantexLeague.DataAccess.Repositories.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.Repositories
{
    public interface ICompetitionRepository : IRepositoryBase<Competition>
    {
        Task<Competition> GetByCodeAsync(string code);
        Task<bool> Exist(string code);
    }
}