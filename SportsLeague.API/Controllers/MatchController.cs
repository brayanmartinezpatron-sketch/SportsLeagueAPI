using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public MatchController(LeagueDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var matches = _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.MatchResult)
            .Include(m => m.Goals)
            .Include(m => m.Cards)
            .ToList();

        return Ok(matches);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Match match)
    {
        _context.Matches.Add(match);
        _context.SaveChanges();
        return Ok(match);
    }
}