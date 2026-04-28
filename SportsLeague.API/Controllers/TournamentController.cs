using Microsoft.AspNetCore.Mvc;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public TournamentController(LeagueDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Tournaments.ToList());
    }

    // GET by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var tournament = _context.Tournaments.Find(id);
        if (tournament == null)
            return NotFound();

        return Ok(tournament);
    }

    // POST
    [HttpPost]
    public IActionResult Create([FromBody] TournamentCreateDto dto)
    {
        var tournament = new Tournament
        {
            Name = dto.Name,
            Location = dto.Location,
            StartDate = dto.StartDate,
            Prize = dto.Prize,
            Status = dto.Status
        };

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        return Ok(tournament);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TournamentCreateDto dto)
    {
        var existing = _context.Tournaments.Find(id);
        if (existing == null)
            return NotFound();

        existing.Name = dto.Name;
        existing.Location = dto.Location;
        existing.StartDate = dto.StartDate;
        existing.Prize = dto.Prize;
        existing.Status = dto.Status;

        _context.SaveChanges();

        return Ok(existing);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var tournament = _context.Tournaments.Find(id);
        if (tournament == null)
            return NotFound();

        _context.Tournaments.Remove(tournament);
        _context.SaveChanges();

        return Ok();
    }
    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(int id, [FromBody] string status)
    {
        var tournament = _context.Tournaments.Find(id);
        if (tournament == null)
            return NotFound();

        tournament.Status = status;
        _context.SaveChanges();

        return Ok(tournament);
    }
    [HttpPost("{id}/teams")]
    public IActionResult AddTeam(int id, [FromBody] int teamId)
    {
        var tournament = _context.Tournaments
            .Include(t => t.Teams)
            .FirstOrDefault(t => t.Id == id);

        if (tournament == null)
            return NotFound();

        var team = _context.Teams.Find(teamId);
        if (team == null)
            return NotFound("Team no existe");

        tournament.Teams.Add(team);
        _context.SaveChanges();

        return Ok(tournament);
    }
    [HttpGet("{id}/teams")]
    public IActionResult GetTournamentTeams(int id)
    {
        var teams = _context.TournamentTeams
            .Include(tt => tt.Team)
            .Where(tt => tt.TournamentId == id)
            .Select(tt => tt.Team)
            .ToList();

        return Ok(teams);
    }
}