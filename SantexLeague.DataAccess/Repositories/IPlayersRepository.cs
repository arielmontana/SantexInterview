using System.Threading.Tasks;
using SantexLeague.DataAccess.Repositories.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.Repositories
{
    public interface IPlayersRepository : IRepositoryBase<Player>
    {
        Task<int> GetCountByLeagueCode(string code);
        
    }
}