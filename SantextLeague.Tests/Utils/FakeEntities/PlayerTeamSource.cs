using System;
using System.Collections.Generic;
using System.Text;
using SantexLeague.Integration.ExternalEntities;

namespace SantextLeague.Tests.Utils.FakeEntities
{
    public class PlayerTeamSource
    {
        private static List<PlayerTeamDto> playerTeamDtos;
        public static List<PlayerTeamDto> PlayerTeams()
        {
            if (playerTeamDtos == null)
            {
                playerTeamDtos = new List<PlayerTeamDto>();
                foreach (var competition in TeamCompetitionSource.teamCompetitions())
                {
                    int i = 0;
                    foreach (var team in competition.teams)
                    {
                        i++;
                        if (team.Players == null)
                        {
                            team.Players = new List<PlayerDto>();
                        }
                        team.Players.Add(
                            new PlayerDto()
                            {
                                id = (team.id * 10) + i,
                                countryOfBirth = $"countryOfBirth_{competition.competition.code}",
                                name = $"name_{competition.competition.code}",
                                dateOfBird = new DateTime(),
                                nationality = $"nationality_{competition.competition.code}",
                                position = $"position_{competition.competition.code}",
                            }); ;
                    }
                }
                
            }
            return playerTeamDtos;
        }
    }
}
