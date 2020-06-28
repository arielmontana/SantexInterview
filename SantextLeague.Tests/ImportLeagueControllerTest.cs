using System.Threading.Tasks;
using ImportLeague.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SantexLeague.Domain;
using SantexLeague.Integration.Services;
using SantexLeague.Services;
using Xunit;

namespace SantextLeague.Tests
{
    public class ImportLeagueControllerTest
    {

        [Fact]
        public async Task WhenImportedCode_AlreadyExistInDB_ShouldReturns409Code() 
        {
            string code = "CL";
            var mockService = new Mock<ICompetitionService>();
            var mockIntegrationService = new Mock<IImportLeagueService>();
            mockService.Setup(service => service.GetByCodeAsync(code))
                .ReturnsAsync(new Competition());
            var controller = new ImportLeagueController(mockIntegrationService.Object, mockService.Object);
            var result = controller.ImportAsync(code).Result as ObjectResult;
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
        }

        [Fact]
        public async Task WhenImportedCode_DoesntExist_ShouldReturns404Code()
        {
            string code = "CL";
            var mockService = new Mock<ICompetitionService>();
            var mockIntegrationService = new Mock<IImportLeagueService>();
            var controller = new ImportLeagueController(mockIntegrationService.Object, mockService.Object);
            var result = controller.ImportAsync(code).Result as ObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }


        [Fact]
        public async Task WhenImportedCode_IsImported_ShouldReturns2012Code()
        {
            string code = "CL";
            var mockService = new Mock<ICompetitionService>();
            var mockIntegrationService = new Mock<IImportLeagueService>();
            mockIntegrationService.Setup(integration => integration.ImportWithCodeLeague(code))
                .ReturnsAsync(new Competition());
            var controller = new ImportLeagueController(mockIntegrationService.Object, mockService.Object);
            var result = controller.ImportAsync(code).Result as ObjectResult;
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }
        
    }
}