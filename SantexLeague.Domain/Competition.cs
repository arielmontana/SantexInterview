using System.Collections.Generic;
using SantexLeague.Domain.Base;

namespace SantexLeague.Domain
{
    public class Competition : BaseEntity
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public string AreaName { get; set; }

        public virtual List<TeamCompetition> TeamsCompetition { get; set; } = new List<TeamCompetition>();
    }
}