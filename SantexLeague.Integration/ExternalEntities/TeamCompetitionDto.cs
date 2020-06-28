using System;
using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
using SantexLeague.Integration.AutoMapper;

namespace SantexLeague.Integration.ExternalEntities
{
    [Serializable]
    public class TeamCompetitionDto
    {

        public CompetitionDto competition { get; set; }

        public List<TeamDto> teams { get; set; }

        public List<TeamCompetitionItemDto> GetItems() 
        {
            return CustomTeamCompetitionItemMapper.Map(this);
        }
    }

    public class TeamCompetitionItemDto 
    { 
        public CompetitionDto competition { get; set; }
        public TeamDto team { get; set; }
    }
}