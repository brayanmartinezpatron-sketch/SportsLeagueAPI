using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.API.DTOs;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SponsorController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public SponsorController(LeagueDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Sponsors.ToList());
    }

    // GET by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var sponsor = _context.Sponsors.Find(id);
        if (sponsor == null)
            return NotFound();

        return Ok(sponsor);
    }

    // POST
    [HttpPost]
    public IActionResult Create([FromBody] Sponsor sponsor)
    {
        _context.Sponsors.Add(sponsor);
        _context.SaveChanges();

        return Ok(sponsor);
    }
    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Sponsor model)
    {
        var existing = _context.Sponsors.Find(id);
        if (existing == null)
            return NotFound();

        existing.Name = model.Name;
        existing.ContactEmail = model.ContactEmail;
        existing.Phone = model.Phone;
        existing.WebsiteUrl = model.WebsiteUrl;

        _context.SaveChanges();

        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var sponsor = _context.Sponsors.Find(id);
        if (sponsor == null)
            return NotFound();

        _context.Sponsors.Remove(sponsor);
        _context.SaveChanges();

        return NoContent();
    }

    // 🔥 GET torneos del sponsor
    [HttpGet("{id}/tournaments")]
    public IActionResult GetTournaments(int id)
    {
        var data = _context.TournamentSponsors
            .Include(ts => ts.Tournament)
            .Where(ts => ts.SponsorId == id)
            .Select(ts => new
            {
                id = ts.Tournament.Id,
                name = ts.Tournament.Name,
                location = ts.Tournament.Location,
                startDate = ts.Tournament.StartDate,
                contractAmount = ts.ContractAmount
            })
            .ToList();

        return Ok(data);
    }

    // 🔥 POST vincular
    [HttpPost("{id}/tournaments")]
    public IActionResult AddTournament(int id, [FromBody] SponsorTournamentDto dto)
    {
        var sponsor = _context.Sponsors.Find(id);
        if (sponsor == null)
            return NotFound("Sponsor no existe");

        var tournament = _context.Tournaments.Find(dto.TournamentId);
        if (tournament == null)
            return NotFound("Tournament no existe");

        var exists = _context.TournamentSponsors
            .Any(x => x.SponsorId == id && x.TournamentId == dto.TournamentId);

        if (exists)
            return Conflict("Ya existe la relación");

        var relation = new TournamentSponsor
        {
            SponsorId = id,
            TournamentId = dto.TournamentId,
            ContractAmount = dto.ContractAmount
        };

        _context.TournamentSponsors.Add(relation);
        _context.SaveChanges();

        return Ok(new
        {
            relation.SponsorId,
            relation.TournamentId,
            relation.ContractAmount
        });
    }
    

        // 🔥 DELETE desvincular
        [HttpDelete("{id}/tournaments/{tid}")]
    public IActionResult RemoveTournament(int id, int tid)
    {
        var relation = _context.TournamentSponsors
            .FirstOrDefault(ts => ts.SponsorId == id && ts.TournamentId == tid);

        if (relation == null)
            return NotFound();

        _context.TournamentSponsors.Remove(relation);
        _context.SaveChanges();

        return NoContent();
    }
}