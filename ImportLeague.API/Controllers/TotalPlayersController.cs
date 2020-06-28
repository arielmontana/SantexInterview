using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantexLeague.API.Responses;
using SantexLeague.Common.Exceptions;
using SantexLeague.Services;

namespace SantexLeague.API.Controllers
{
    [Route("api/total-players")]
    [ApiController]
    public class TotalPlayersController : ControllerBase
    {
        private readonly IPlayersService playersService;
        private readonly ICompetitionService competitionService;

        public TotalPlayersController(IPlayersService playersService,
                                      ICompetitionService competitionService)
        {
            this.playersService = playersService;
            this.competitionService = competitionService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetCount(string code)
        {
            var playerCountDto = new PlayerCountDto();
            if (!await competitionService.Exist(code)) return StatusCode(StatusCodes.Status404NotFound, new StatusMessage("Not Found"));
            playerCountDto.total = await playersService.GetCountByLeagueCode(code);
            return Ok(playerCountDto);
        }
    }
}