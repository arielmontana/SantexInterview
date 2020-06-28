using System.Collections.Generic;
using SantexLeague.Integration.AutoMapper;

namespace SantexLeague.Integration.ExternalEntities
{
    public class PlayerTeamDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string tla { get; set; }
        public string shortName { get; set; }
        public AreaDto area { get; set; }
        public string email { get; set; }
        public List<PlayerDto> squad { get; set; }

        public List<PlayerTeamItemDto> GetItems() 
        {
            return CustomPlayerTeamItemMapper.Map(this);
        }

       

    }

    public class PlayerTeamItemDto
    {
        public TeamDto Team { get; set; }
        public PlayerDto Player { get; set; }
    }
}