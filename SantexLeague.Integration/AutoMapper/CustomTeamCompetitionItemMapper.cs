using System.Collections.Generic;
using SantexLeague.Integration.ExternalEntities;

namespace SantexLeague.Integration.AutoMapper
{
    public class CustomTeamCompetitionItemMapper
    {
        public static List<TeamCompetitionItemDto>  Map(TeamCompetitionDto teamCompetitionDto) 
        {
            var list = new List<TeamCompetitionItemDto>();
            teamCompetitionDto.teams
                .ForEach(x => list.Add(new TeamCompetitionItemDto() { team = x, competition = teamCompetitionDto.competition }));
            return list;
        }
    }
}