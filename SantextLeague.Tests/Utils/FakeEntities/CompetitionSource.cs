using System;
using System.Collections.Generic;
using System.Text;
using SantexLeague.Integration.ExternalEntities;

namespace SantextLeague.Tests.Utils.FakeEntities
{
    public class CompetitionSource
    {
        private static List<CompetitionDto> competitionDtos;
        public static List<CompetitionDto> competitions() 
        {
            if (competitionDtos == null) 
            {
                competitionDtos = new List<CompetitionDto>();
                competitionDtos.Add(new CompetitionDto() { id = 2000, area = new AreaDto() { name = "Brazil" }, name = "Brazilian Division One", code = "BSA" });
                competitionDtos.Add(new CompetitionDto() { id = 2001, area = new AreaDto() { name = "England" }, name = "Premier League", code = "PL" });
                competitionDtos.Add(new CompetitionDto() { id = 2002, area = new AreaDto() { name = "England" }, name = "Championship", code = "ELC" });
                competitionDtos.Add(new CompetitionDto() { id = 2003, area = new AreaDto() { name = "Europe" }, name = "Champions League", code = "CL" });
                competitionDtos.Add(new CompetitionDto() { id = 2004, area = new AreaDto() { name = "Europe" }, name = "European Championshin", code = "EC" });
                competitionDtos.Add(new CompetitionDto() { id = 2005, area = new AreaDto() { name = "France" }, name = "France League 1", code = "FL1" });
                competitionDtos.Add(new CompetitionDto() { id = 2006, area = new AreaDto() { name = "Germany" }, name = "Bundesliga", code = "BL1" });
                competitionDtos.Add(new CompetitionDto() { id = 2007, area = new AreaDto() { name = "Italy" }, name = "Italy Serie A", code = "SA" });
                competitionDtos.Add(new CompetitionDto() { id = 2008, area = new AreaDto() { name = "Netherlands" }, name = "Eredivise", code = "DED" });
                competitionDtos.Add(new CompetitionDto() { id = 2009, area = new AreaDto() { name = "Portugal" }, name = "Portuguese Primeira Division", code = "PPL" });
                competitionDtos.Add(new CompetitionDto() { id = 2010, area = new AreaDto() { name = "Spain" }, name = "Primera Division", code = "PD" });
                competitionDtos.Add(new CompetitionDto() { id = 2011, area = new AreaDto() { name = "World" }, name = "World Cup", code = "WC" });
            }
            return competitionDtos;
        }
    }
}
