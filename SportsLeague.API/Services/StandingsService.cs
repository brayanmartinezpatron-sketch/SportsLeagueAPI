using Microsoft.EntityFrameworkCore;
using SportsLeague.API.DTOs.Response;
using SportsLeague.API.Interfaces;
using SportsLeague.DataAccess.Context;

namespace SportsLeague.API.Services;

public class StandingsService : IStandingsService
{
    private readonly LeagueDbContext _context;

    public StandingsService(LeagueDbContext context)
    {
        _context = context;
    }

    public async Task<List<StandingDTO>> GetStandingsAsync()
    {
        var teams = await _context.Teams.ToListAsync();

        var standings = teams.Select((team, index) => new StandingDTO
        {
            Position = index + 1,
            TeamId = team.Id,
            TeamName = team.Name,
            MatchesPlayed = 0,
            Wins = 0,
            Draws = 0,
            Losses = 0,
            GoalsFor = 0,
            GoalsAgainst = 0,
            GoalDifference = 0,
            Points = 0
        }).ToList();

        return standings;
    }

    public async Task<List<TopScorerDTO>> GetTopScorersAsync()
    {
        var players = await _context.Players
            .Include(p => p.Team)
            .ToListAsync();

        var scorers = players.Select(p => new TopScorerDTO
        {
            PlayerId = p.Id,
            PlayerName = p.Name,
            TeamName = p.Team.Name,
            Goals = 0,
            Penalties = 0,
            MatchesWithGoals = 0
        }).ToList();

        return scorers;
    }

    public async Task<List<CardStatsDTO>> GetCardStatsAsync()
    {
        var players = await _context.Players
            .Include(p => p.Team)
            .ToListAsync();

        var cards = players.Select(p => new CardStatsDTO
        {
            PlayerId = p.Id,
            PlayerName = p.Name,
            TeamName = p.Team.Name,
            YellowCards = 0,
            RedCards = 0,
            TotalCards = 0
        }).ToList();

        return cards;
    }
}