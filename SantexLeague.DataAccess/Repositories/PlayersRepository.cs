using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SantexLeague.DataAccess.EntityFramework;
using SantexLeague.DataAccess.Repositories.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.Repositories
{
    public class PlayersRepository :  RepositoryBase<Player>,  IPlayersRepository
    {
        public PlayersRepository(SantexContext dbContext): base(dbContext)
        {
        }

        public async Task<int> GetCountByLeagueCode(string code)
        {
            return await (from p in dbContext.Players
                     join tp in dbContext.PlayersTeam on p.Id equals tp.PlayerId
                     join t in dbContext.Teams on tp.TeamId equals t.Id
                     join tc in dbContext.TeamCompetitions on t.Id equals tc.TeamId
                     join c in dbContext.Competitions on tc.CompetitionId equals c.Id
                     where string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase)
                     select p).CountAsync();
        }
    }
}
