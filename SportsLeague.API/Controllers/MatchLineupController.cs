using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs;
using SportsLeague.API.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/match/{matchId}/lineup")]
public class MatchLineupController : ControllerBase
{
    private readonly MatchLineupService _service;

    public MatchLineupController(MatchLineupService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPlayer(
        int matchId,
        CreateMatchLineupDto dto)
    {
        var result = await _service.AddPlayerAsync(matchId, dto);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLineup(int matchId)
    {
        var result = await _service.GetLineupAsync(matchId);

        return Ok(result);
    }

    [HttpGet("team/{teamId}")]
    public async Task<IActionResult> GetLineupByTeam(
        int matchId,
        int teamId)
    {
        var result = await _service.GetLineupByTeamAsync(matchId, teamId);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int matchId,
        int id)
    {
        await _service.DeleteAsync(matchId, id);

        return NoContent();
    }
}