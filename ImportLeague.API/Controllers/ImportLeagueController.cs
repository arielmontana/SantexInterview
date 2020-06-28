using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantexLeague.Common.Exceptions;
using SantexLeague.Integration.Services;
using SantexLeague.Services;

namespace ImportLeague.API.Controllers
{
    [Route("api/import-league")]
    [ApiController]
    public class ImportLeagueController : ControllerBase
    {
        private readonly IImportLeagueService importLeagueService;
        private readonly ICompetitionService competitionService;

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
            var competitionInDb = await competitionService.GetByCodeAsync(code);
            if (competitionInDb != null) return StatusCode(StatusCodes.Status409Conflict, new StatusMessage("League already imported"));

            var competition = importLeagueService.ImportWithCodeLeague(code).Result;
            if (competition == null) return StatusCode(StatusCodes.Status404NotFound, new StatusMessage("Not Found"));

            await competitionService.SaveAsync(competition);
            return StatusCode(StatusCodes.Status201Created, new StatusMessage("Successfully imported"));
        }
    }
}