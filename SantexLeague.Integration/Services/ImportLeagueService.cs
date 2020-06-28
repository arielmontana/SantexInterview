using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SantexLeague.Domain;
using SantexLeague.Integration.ExternalEntities;
using SantexLeague.Integration.HttpUtilities;
using SantexLeague.Integration.Services;
using Serilog;

namespace SantexLeague.Integration
{
    public class ImportLeagueService : IImportLeagueService
    {
        private readonly IHttpManager httpManager;
        private readonly IMapper mapper;
        private const string baseUrl = "https://api.football-data.org/v2";
        private const string competitionUrl = "{0}/competitions/{1}";
        private const string teamsCompetitionUrl = "{0}/competitions/{1}/teams";
        private const string teamPlayersUrl = "{0}/teams/{1}";

        public ImportLeagueService(IHttpManager httpManager, IMapper mapper)
        {
            this.httpManager = httpManager;
            this.mapper = mapper;
        }

        public async Task<Competition> ImportWithCodeLeague(string code)
        {
            try
            {
                return await Task.Run(() => {
                    var competition = GetCompetition(code);
                    if (competition == null) return null;
                    GetAndLoadTeamCompetition(competition);
                    GetAndLoadTeamPlayers(competition);
                    return competition;
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Competition GetCompetition(string code)
        {
            try
            {
                var compUrl = string.Format(competitionUrl, baseUrl, code.ToUpper());
                var competitionDto = httpManager.Get<CompetitionDto>(compUrl).Result;
                return mapper.Map<Competition>(competitionDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void GetAndLoadTeamCompetition(Competition competition)
        {
            var tcUrl = string.Format(teamsCompetitionUrl, baseUrl, competition.ExternalId);
            var teamsCompetitionDto = httpManager.Get<TeamCompetitionDto>(tcUrl).Result;
            var itemTeamCompetition = teamsCompetitionDto.GetItems();
            foreach (var item in itemTeamCompetition)
            {
                competition.TeamsCompetition.Add(mapper.Map<TeamCompetition>(item));
            }
        }

        private void GetAndLoadTeamPlayers(Competition competition)
        {
            var teams = competition.TeamsCompetition.Select(x => x.Team).ToList();
            do
            {
                 
                var team = teams.Where(x => x.PlayersTeam == null).First();
                var tpUrl = string.Format(teamPlayersUrl, baseUrl, team.ExternalId);
                try
                {
                    var playerTeam = httpManager.Get<PlayerTeamDto>(tpUrl).Result;
                    team.PlayersTeam = new List<PlayerTeam>();
                    var playerTeamItem = playerTeam.GetItems();
                    foreach (var item in playerTeamItem)
                    {
                        var map = mapper.Map<PlayerTeam>(item);
                        team.PlayersTeam.Add(map);
                    }
                }
                catch (Exception ex)
                {
                    Log.Information(ex, tpUrl);
                }
            }
            while (teams.Any(x => x.PlayersTeam == null));
        }
    }
}