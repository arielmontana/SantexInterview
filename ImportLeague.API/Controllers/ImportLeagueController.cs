using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantexLeague.Integration.Services;
using SantexLeague.Services;
using Serilog;

namespace ImportLeague.API.Controllers
{
    [Route("api/import-league")]
    [ApiController]
    public class ImportLeagueController : ControllerBase
    {
        private readonly IImportLeagueService importLeagueService;
        private readonly ICompetitionService competitionService;
        private const string errorMessage = "Server Error";
        public ImportLeagueController(IImportLeagueService importLeagueService,
                                      ICompetitionService competitionService)
        {
            this.importLeagueService = importLeagueService;
            this.competitionService = competitionService;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> ImportAsync(string code)
        {
            try
            {
                var competitionInDb = await competitionService.GetByCodeAsync(code);
                if (competitionInDb != null) return StatusCode(StatusCodes.Status409Conflict, "League already imported");

                var competition = importLeagueService.ImportWithCodeLeague(code).Result;
                if (competition == null) return StatusCode(StatusCodes.Status404NotFound, "Not Found");

                await competitionService.SaveAsync(competition);
                return StatusCode(StatusCodes.Status201Created, $"Successfully imported'");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status504GatewayTimeout, "Server Error");
            }
        }
    }
}