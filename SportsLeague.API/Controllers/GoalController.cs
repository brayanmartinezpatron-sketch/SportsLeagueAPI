using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public GoalController(LeagueDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var goals = _context.Goals
            .Include(g => g.Player)
            .Include(g => g.Match)
            .ToList();

        return Ok(goals);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Goal goal)
    {
        _context.Goals.Add(goal);
        _context.SaveChanges();
        return Ok(goal);
    }
}