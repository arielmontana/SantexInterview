using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SantexLeague.Integration.ExternalEntities
{
    [Serializable]
    public class TeamCompetitionDto
    {

        public CompetitionDto competition { get; set; }

        public List<TeamDto> teams { get; set; }

        public List<TeamCompetitionItemDto> GetItems() 
        {
            var list = new List<TeamCompetitionItemDto>();
            teams.ForEach(x => list.Add(new TeamCompetitionItemDto() { team = x, competition = this.competition }));
            return list;
        }
    }

    public class TeamCompetitionItemDto 
    { 
        public CompetitionDto competition { get; set; }
        public TeamDto team { get; set; }
    }
}