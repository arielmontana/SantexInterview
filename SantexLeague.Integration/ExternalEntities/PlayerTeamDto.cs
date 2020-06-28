using System.Collections.Generic;

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
            var lista = new List<PlayerTeamItemDto>();
            squad.ForEach(x => lista.Add(new PlayerTeamItemDto() { Player = x, Team = this.GetTeam()}));
            return lista;
        }

        private TeamDto _teamDto;
        private TeamDto GetTeam() 
        {
            if (_teamDto == null) 
            {
                _teamDto = new TeamDto() 
                { id = this.id, email = this.email, name = this.name, area = this.area, shortName = this.shortName, tla = this.tla };
            }
            return _teamDto;
        }

    }

    public class PlayerTeamItemDto
    {
        public TeamDto Team { get; set; }
        public PlayerDto Player { get; set; }
    }
}