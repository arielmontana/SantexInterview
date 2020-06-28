using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SantexLeague.Integration.ExternalEntities;
using SantexLeague.Integration.HttpUtilities;
using SantextLeague.Tests.Utils;
using SantextLeague.Tests.Utils.FakeEntities;
using Xunit;

namespace SantextLeague.Tests
{
    public class IntegrationLayerTests : IDisposable
    {
        private readonly IMapper automapper;

        public IntegrationLayerTests()
        {
            this.automapper = AutoMapperFake.Get();
        }

        public void Dispose()
        {
        }

        [Fact]
        public async Task WhenHttpManagerIsExecuted_ManagerReturnsCorrectCompetitionDto()
        {
            var url = "https://validurl.com";
            var competition = CompetitionSource.competitions().Where(x => x.code == "CL").FirstOrDefault();
            var httpClientMock = IHttpClientFactoryFake.Get(competition);
            var configurationMock = IConfigurationFake.Get();

            var httpManagerMock = new HttpManager(configurationMock, httpClientMock);
            var competitionResult = await httpManagerMock.Get<CompetitionDto>(url);

            Assert.IsType<CompetitionDto>(competitionResult);
            Assert.NotNull(competitionResult);
            Assert.Equal("CL", competitionResult.code);
            Assert.Equal("Champions League", competitionResult.name);
            Assert.NotNull(competitionResult.area);
            Assert.Equal("Europe", competitionResult.area.name);
        }

        [Fact]
        public async Task WhenHttpManagerIsExecuted_ManagerReturnsCorrectTeamCompetitionDto()
        {
            var url = "https://validurl.com";
            var teamCompetition = TeamCompetitionSource.teamCompetitions().Where(x => x.competition.code == "CL").ToList();
            var httpClientMock = IHttpClientFactoryFake.Get(teamCompetition);
            var configurationMock = IConfigurationFake.Get();

            var httpManagerMock = new HttpManager(configurationMock, httpClientMock);
            var competitionResult = await httpManagerMock.Get<List<TeamCompetitionDto>>(url);

            Assert.IsType<List<TeamCompetitionDto>>(competitionResult);
            Assert.Equal(12, CompetitionSource.competitions().Count());
            var expectedCompetition = CompetitionSource.competitions().Where(x => x.code == "CL").FirstOrDefault();
            foreach (var item in competitionResult)
            {
                Assert.Equal(expectedCompetition.code, item.competition.code);
            }
        }
    }
}