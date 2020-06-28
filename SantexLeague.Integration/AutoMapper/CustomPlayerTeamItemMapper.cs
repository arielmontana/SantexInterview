using System.Collections.Generic;
using SantexLeague.Integration.ExternalEntities;

namespace SantexLeague.Integration.AutoMapper
{
    public class CustomPlayerTeamItemMapper
    {
        public static List<PlayerTeamItemDto> Map(PlayerTeamDto playerTeam)
        {
            var lista = new List<PlayerTeamItemDto>();
            var team = GetTeam(playerTeam);
            playerTeam.squad.ForEach(x => lista.Add(new PlayerTeamItemDto() { Player = x, Team = team }));
            return lista;
        }

        private static TeamDto GetTeam(PlayerTeamDto playerTeam)
        {
            return new TeamDto()
            { id = playerTeam.id, email = playerTeam.email, name = playerTeam.name, area = playerTeam.area, shortName = playerTeam.shortName, tla = playerTeam.tla };
        }
    }
}