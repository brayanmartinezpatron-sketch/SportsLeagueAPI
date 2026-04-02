using Microsoft.AspNetCore.Mvc;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.API.DTOs;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefereeController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public RefereeController(LeagueDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Referees.ToList());
    }

    // GET by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var referee = _context.Referees.Find(id);
        if (referee == null)
            return NotFound();

        return Ok(referee);
    }

    // POST
    [HttpPost]
    public IActionResult Create([FromBody] RefereeCreateDto dto)
    {
        var referee = new Referee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Nationality = dto.Nationality
        };

        _context.Referees.Add(referee);
        _context.SaveChanges();

        return Ok(referee);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] RefereeCreateDto dto)
    {
        var existing = _context.Referees.Find(id);
        if (existing == null)
            return NotFound();

        existing.FirstName = dto.FirstName;
        existing.LastName = dto.LastName;
        existing.Nationality = dto.Nationality;

        _context.SaveChanges();

        return Ok(existing);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var referee = _context.Referees.Find(id);
        if (referee == null)
            return NotFound();

        _context.Referees.Remove(referee);
        _context.SaveChanges();

        return Ok();
    }
}