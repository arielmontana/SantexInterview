using System.Collections.Generic;
using SantexLeague.Domain.Base;

namespace SantexLeague.Domain
{
    public class Team : BaseEntity
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Tla { get; set; }
        public string ShortName { get; set; }
        public string AreaName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<PlayerTeam> PlayersTeam { get; set; }
    }
}