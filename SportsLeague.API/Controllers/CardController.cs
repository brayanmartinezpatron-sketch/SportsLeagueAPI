using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public CardController(LeagueDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var cards = _context.Cards
            .Include(c => c.Player)
            .Include(c => c.Match)
            .ToList();

        return Ok(cards);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Card card)
    {
        _context.Cards.Add(card);
        _context.SaveChanges();
        return Ok(card);
    }
}