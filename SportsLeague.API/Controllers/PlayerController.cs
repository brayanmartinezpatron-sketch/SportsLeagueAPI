using Microsoft.AspNetCore.Mvc;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.API.DTOs;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public PlayerController(LeagueDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Players.ToList());
    }

    // GET by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var player = _context.Players.Find(id);
        if (player == null)
            return NotFound();

        return Ok(player);
    }

    // POST
    [HttpPost]
    public IActionResult Create([FromBody] PlayerCreateDto dto)
    {
        var player = new Player
        {
            Name = dto.Name,
            Age = dto.Age,
            Position = dto.Position,
            TeamId = dto.TeamId
        };

        _context.Players.Add(player);
        _context.SaveChanges();

        return Ok(player);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] PlayerCreateDto dto)
    {
        var existing = _context.Players.Find(id);
        if (existing == null)
            return NotFound();

        existing.Name = dto.Name;
        existing.Age = dto.Age;
        existing.Position = dto.Position;
        existing.TeamId = dto.TeamId;

        _context.SaveChanges();

        return Ok(existing);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var player = _context.Players.Find(id);
        if (player == null)
            return NotFound();

        _context.Players.Remove(player);
        _context.SaveChanges();

        return Ok();
    }
}