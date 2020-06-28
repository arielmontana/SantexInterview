using System;
using System.Collections.Generic;
using System.Text;

namespace SantexLeague.Integration.ExternalEntities
{
    public class TeamDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public AreaDto area { get; set; }
        public string shortName { get; set; }
        public string tla { get; set; }
        public string email { get; set; }
        public List<PlayerDto> Players { get; set; }
    }
}
