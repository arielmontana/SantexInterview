using System.Threading.Tasks;

namespace SantexLeague.Services
{
    public interface IPlayersService
    {
        Task<int> GetCountByLeagueCode(string code);
    }
}