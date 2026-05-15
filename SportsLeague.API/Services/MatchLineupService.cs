using Microsoft.EntityFrameworkCore;
using SportsLeague.API.DTOs;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Services;

public class MatchLineupService
{
    private readonly LeagueDbContext _context;

    public MatchLineupService(LeagueDbContext context)
    {
        _context = context;
    }

    public async Task<MatchLineupDto> AddPlayerAsync(int matchId, CreateMatchLineupDto dto)
    {
        var match = await _context.Matches
            .FirstOrDefaultAsync(m => m.Id == matchId);

        if (match == null)
            throw new Exception($"No se encontró el partido con ID {matchId}");

        var player = await _context.Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(p => p.Id == dto.PlayerId);

        if (player == null)
            throw new Exception($"No se encontró el jugador con ID {dto.PlayerId}");

        if (player.TeamId != match.HomeTeamId &&
            player.TeamId != match.AwayTeamId)
        {
            throw new Exception("El jugador no pertenece a ninguno de los equipos del partido");
        }

        var exists = await _context.MatchLineups
            .AnyAsync(ml => ml.MatchId == matchId &&
                            ml.PlayerId == dto.PlayerId);

        if (exists)
            throw new Exception("El jugador ya está registrado en la alineación de este partido");

        if (dto.IsStarter)
        {
            var startersCount = await _context.MatchLineups
                .CountAsync(ml =>
                    ml.MatchId == matchId &&
                    ml.IsStarter &&
                    ml.Player.TeamId == player.TeamId);

            if (startersCount >= 11)
                throw new Exception("El equipo ya tiene 11 titulares registrados en este partido");
        }

       
        var lineup = new MatchLineup
        {
            MatchId = matchId,
            PlayerId = dto.PlayerId,
            IsStarter = dto.IsStarter,
            Position = dto.Position
        };

        _context.MatchLineups.Add(lineup);
        await _context.SaveChangesAsync();

        return new MatchLineupDto
        {
            Id = lineup.Id,
            MatchId = lineup.MatchId,
            PlayerId = player.Id,
            PlayerName = player.Name,
            TeamName = player.Team.Name,
            IsStarter = lineup.IsStarter,
            Position = lineup.Position
        };
    }

    public async Task<List<MatchLineupDto>> GetLineupAsync(int matchId)
    {
        return await _context.MatchLineups
            .Include(ml => ml.Player)
            .ThenInclude(p => p.Team)
            .Where(ml => ml.MatchId == matchId)
            .Select(ml => new MatchLineupDto
            {
                Id = ml.Id,
                MatchId = ml.MatchId,
                PlayerId = ml.PlayerId,
                PlayerName = ml.Player.Name,
                TeamName = ml.Player.Team.Name,
                IsStarter = ml.IsStarter,
                Position = ml.Position
            })
            .ToListAsync();
    }

    public async Task<List<MatchLineupDto>> GetLineupByTeamAsync(int matchId, int teamId)
    {
        return await _context.MatchLineups
            .Include(ml => ml.Player)
            .ThenInclude(p => p.Team)
            .Where(ml => ml.MatchId == matchId &&
                         ml.Player.TeamId == teamId)
            .Select(ml => new MatchLineupDto
            {
                Id = ml.Id,
                MatchId = ml.MatchId,
                PlayerId = ml.PlayerId,
                PlayerName = ml.Player.Name,
                TeamName = ml.Player.Team.Name,
                IsStarter = ml.IsStarter,
                Position = ml.Position
            })
            .ToListAsync();
    }

    public async Task DeleteAsync(int matchId, int id)
    {
        var lineup = await _context.MatchLineups
            .FirstOrDefaultAsync(ml =>
                ml.MatchId == matchId &&
                ml.Id == id);

        if (lineup == null)
            throw new Exception("No se encontró el registro");

        _context.MatchLineups.Remove(lineup);
        await _context.SaveChangesAsync();
    }
}