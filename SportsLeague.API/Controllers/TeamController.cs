using Microsoft.AspNetCore.Mvc;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.API.DTOs;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public TeamController(LeagueDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public IActionResult GetAll()
    {
        var teams = _context.Teams.ToList();
        return Ok(teams);
    }

    // GET by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var team = _context.Teams.Find(id);
        if (team == null)
            return NotFound();

        return Ok(team);
    }

    // POST
    [HttpPost]
    public IActionResult Create([FromBody] TeamCreateDto dto)
    {
        var team = new Team
        {
            Name = dto.Name,
            City = dto.City,
            Stadium = dto.Stadium,
            LogoUrl = dto.LogoUrl,
            FoundedDate = dto.FoundedDate
        };

        _context.Teams.Add(team);
        _context.SaveChanges();

        return Ok(team);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Team team)
    {
        var existing = _context.Teams.Find(id);
        if (existing == null)
            return NotFound();

        existing.Name = team.Name;
        existing.City = team.City;
        existing.Stadium = team.Stadium;
        existing.LogoUrl = team.LogoUrl;
        existing.FoundedDate = team.FoundedDate;

        _context.SaveChanges();

        return Ok(existing);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var team = _context.Teams.Find(id);
        if (team == null)
            return NotFound();

        _context.Teams.Remove(team);
        _context.SaveChanges();

        return Ok();
    }
}