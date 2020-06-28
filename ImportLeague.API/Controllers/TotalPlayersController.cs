using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantexLeague.API.Responses;
using SantexLeague.Services;
using Serilog;

namespace SantexLeague.API.Controllers
{
    [Route("api/total-players")]
    [ApiController]
    public class TotalPlayersController : ControllerBase
    {
        private readonly IPlayersService playersService;
        private readonly ICompetitionService competitionService;
        private const string errorMessage = "Server Error";

        public TotalPlayersController(IPlayersService playersService,
                                      ICompetitionService competitionService)
        {
            this.playersService = playersService;
            this.competitionService = competitionService;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> GetCount(string code)
        {
            try
            {
                throw new Exception();
                var playerCountDto = new PlayerCountDto();
                if (!await competitionService.Exist(code)) return StatusCode(StatusCodes.Status404NotFound, "Not Found");
                playerCountDto.total = await playersService.GetCountByLeagueCode(code);
                return Ok(playerCountDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status504GatewayTimeout, errorMessage);
            }
        }
    }
}