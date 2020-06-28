using System.Linq;
using System.Threading.Tasks;
using SantexLeague.DataAccess.Repositories;
using SantexLeague.DataAccess.UnitsOfWork;
using SantexLeague.Domain;

namespace SantexLeague.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IUnitOfWork unitOfWork;
        public CompetitionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Exist(string code)
        {
            return await unitOfWork.CompetitionRepository.Exist(code);
        }

        public async Task<Competition> GetByCodeAsync(string code)
        {
            return await unitOfWork.CompetitionRepository.GetByCodeAsync(code);
        }

        public async Task SaveAsync(Competition competition)
        {
            EnsureRelations(competition);
            await unitOfWork.CompetitionRepository.AddAsync(competition);
            await unitOfWork.CommitAsync();
        }

        private void EnsureRelations(Competition competition) 
        {
            competition.TeamsCompetition.ForEach(x => x.Competition = competition);
            foreach (var (team, playerTeam) in from teamCompetition in competition.TeamsCompetition
                                               let team = teamCompetition.Team
                                               from playerTeam in team.PlayersTeam
                                               select (team, playerTeam))
            {
                playerTeam.Team = team;
            }
        }
    }
}