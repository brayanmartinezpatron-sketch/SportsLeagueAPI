using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.Interfaces;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StandingsController : ControllerBase
{
    private readonly IStandingsService _standingsService;

    public StandingsController(IStandingsService standingsService)
    {
        _standingsService = standingsService;
    }

    [HttpGet("table")]
    public async Task<IActionResult> GetStandings()
    {
        var result = await _standingsService.GetStandingsAsync();
        return Ok(result);
    }

    [HttpGet("topscorers")]
    public async Task<IActionResult> GetTopScorers()
    {
        var result = await _standingsService.GetTopScorersAsync();
        return Ok(result);
    }

    [HttpGet("cards")]
    public async Task<IActionResult> GetCardStats()
    {
        var result = await _standingsService.GetCardStatsAsync();
        return Ok(result);
    }
}