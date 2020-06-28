using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SantexLeague.API.Controllers;
using SantexLeague.API.Responses;
using SantexLeague.Services;
using Xunit;

namespace SantextLeague.Tests
{
    public class TotalPlayersControllerTests
    {
        [Fact]
        public async Task WhenImportedCode_DoesntExist_ShouldReturns404Code()
        {
            string code = "CL";
            var mockPlayerService = new Mock<IPlayersService>();
            var mockCompetitionService = new Mock<ICompetitionService>();
            mockCompetitionService.Setup(service => service.Exist(code))
                .ReturnsAsync(false);
            var controller = new TotalPlayersController(mockPlayerService.Object, mockCompetitionService.Object);
            var result = await controller.GetCount(code) as ObjectResult; // ImportAsync(code).Result as ObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task WhenImportedCode_Exists_ShouldReturns200Code()
        {
            string code = "CL";
            var mockPlayerService = new Mock<IPlayersService>();
            var mockCompetitionService = new Mock<ICompetitionService>();
            mockCompetitionService.Setup(service => service.Exist(code))
                .ReturnsAsync(true);
            mockPlayerService.Setup(service => service.GetCountByLeagueCode(code))
                .ReturnsAsync(10);
            var controller = new TotalPlayersController(mockPlayerService.Object, mockCompetitionService.Object);
            var result = await controller.GetCount(code) as ObjectResult; 
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task WhenImportedCode_Exists_ShouldReturnsTeamsAmount()
        {
            string code = "CL";
            var mockPlayerService = new Mock<IPlayersService>();
            var mockCompetitionService = new Mock<ICompetitionService>();
            mockCompetitionService.Setup(service => service.Exist(code))
                .ReturnsAsync(true);
            mockPlayerService.Setup(service => service.GetCountByLeagueCode(code))
                .ReturnsAsync(10);
            var controller = new TotalPlayersController(mockPlayerService.Object, mockCompetitionService.Object);
            var result = await controller.GetCount(code) as OkObjectResult;
            var teamsAmount = Assert.IsType<PlayerCountDto>(result.Value);
            Assert.Equal(10, teamsAmount.total);
        }
    }
}
