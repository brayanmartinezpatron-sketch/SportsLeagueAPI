using Microsoft.AspNetCore.Mvc;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.API.DTOs;
using System.Linq;

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
            TeamId = dto.TeamId,

            DateOfBirth = dto.DateOfBirth
        };

        _context.Players.Add(player);
        _context.SaveChanges();

        return Ok(player);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Player player)
    {
        var existing = _context.Players.Find(id);
        if (existing == null)
            return NotFound();

        existing.Name = player.Name;
        existing.Age = player.Age;
        existing.Position = player.Position;
        existing.TeamId = player.TeamId;

        existing.DateOfBirth = player.DateOfBirth;

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
    [HttpGet("position/{position}")]
    public IActionResult GetByPosition(string position)
    {
        var players = _context.Players
            .Where(p => p.Position == position)
            .ToList();

        return Ok(players);
    }
}