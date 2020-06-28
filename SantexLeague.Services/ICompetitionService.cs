using System.Threading.Tasks;
using SantexLeague.Domain;

namespace SantexLeague.Services
{
    public interface ICompetitionService
    {
        Task<Competition> GetByCodeAsync(string code);
        Task<bool> Exist(string code);
        Task SaveAsync(Competition teamCompetition);
    }
}