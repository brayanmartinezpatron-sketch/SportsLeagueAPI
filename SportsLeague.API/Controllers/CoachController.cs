using Microsoft.AspNetCore.Mvc;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.API.DTOs;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoachController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public CoachController(LeagueDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Coaches.ToList());
    }

    // GET by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var coach = _context.Coaches.Find(id);
        if (coach == null)
            return NotFound();

        return Ok(coach);
    }

    // POST
    [HttpPost]
    public IActionResult Create([FromBody] CoachCreateDto dto)
    {
        var coach = new Coach
        {
            Name = dto.Name,
            ExperienceYears = dto.ExperienceYears,
            TeamId = dto.TeamId
        };

        _context.Coaches.Add(coach);
        _context.SaveChanges();

        return Ok(coach);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CoachCreateDto dto)
    {
        var existing = _context.Coaches.Find(id);
        if (existing == null)
            return NotFound();

        existing.Name = dto.Name;
        existing.ExperienceYears = dto.ExperienceYears;
        existing.TeamId = dto.TeamId;

        _context.SaveChanges();

        return Ok(existing);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var coach = _context.Coaches.Find(id);
        if (coach == null)
            return NotFound();

        _context.Coaches.Remove(coach);
        _context.SaveChanges();

        return Ok();
    }
}