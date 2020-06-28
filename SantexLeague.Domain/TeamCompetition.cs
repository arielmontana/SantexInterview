using System;
using System.Collections.Generic;
using System.Text;
using SantexLeague.Domain.Base;

namespace SantexLeague.Domain
{
    public class TeamCompetition : BaseEntity
    {
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
