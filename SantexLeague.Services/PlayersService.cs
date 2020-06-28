using System.Threading.Tasks;
using SantexLeague.DataAccess.Repositories;

namespace SantexLeague.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IPlayersRepository playersRepository;
        public PlayersService(IPlayersRepository playersRepository)
        {
            this.playersRepository = playersRepository;
        }
        public Task<int> GetCountByLeagueCode(string code)
        {
            return this.playersRepository.GetCountByLeagueCode(code);
        }
    }
}