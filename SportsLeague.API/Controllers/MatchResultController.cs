using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchResultController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public MatchResultController(LeagueDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var results = _context.MatchResults
            .Include(r => r.Match)
            .ToList();

        return Ok(results);
    }

    [HttpPost]
    public IActionResult Create([FromBody] MatchResult result)
    {
        _context.MatchResults.Add(result);
        _context.SaveChanges();
        return Ok(result);
    }
}