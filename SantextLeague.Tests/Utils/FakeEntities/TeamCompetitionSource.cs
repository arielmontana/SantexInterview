using System;
using System.Collections.Generic;
using System.Text;
using SantexLeague.Integration.ExternalEntities;

namespace SantextLeague.Tests.Utils.FakeEntities
{
    public class TeamCompetitionSource
    {
        private static List<TeamCompetitionDto> _teamCompetitions;
        public static List<TeamCompetitionDto> teamCompetitions()
        {
            if (_teamCompetitions == null)
            {
                _teamCompetitions = new List<TeamCompetitionDto>();
                foreach (var competitionItem in CompetitionSource.competitions())
                {
                    _teamCompetitions.Add(new TeamCompetitionDto() { competition = competitionItem, teams = GetTeams(competitionItem) });
                } 
            }
            return _teamCompetitions;
        }
        private static List<TeamDto> GetTeams(CompetitionDto competition) 
        {
            Random rnd = new Random();
            var teamsNumber = rnd.Next(5, 20);
            var teams = new List<TeamDto>();
            for (int i = 0; i < teamsNumber; i++)
            {
                teams.Add(new TeamDto()
                {
                    area = new AreaDto() { name = $"_{competition.code}" },
                    name = $"name_{competition.code}",
                    email = $"{competition.code}@{competition.code}.com",
                    Players = null,
                    shortName = $"sortname_{competition.code}",
                    tla = $"tla_{competition.code}",
                    id = (competition.id * 100) + i
                }); ;
            }
            return teams;
        }
        
    }
}
