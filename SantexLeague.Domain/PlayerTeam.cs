using System;
using System.Collections.Generic;
using System.Text;
using SantexLeague.Domain.Base;

namespace SantexLeague.Domain
{
    public class PlayerTeam : BaseEntity
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
