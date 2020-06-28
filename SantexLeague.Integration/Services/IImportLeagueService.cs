using System.Threading.Tasks;
using SantexLeague.Domain;

namespace SantexLeague.Integration.Services
{
    public interface IImportLeagueService
    {
        Task<Competition> ImportWithCodeLeague(string code);
    }
}