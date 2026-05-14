using SportsLeague.API.DTOs.Response;

namespace SportsLeague.API.Interfaces;

public interface IStandingsService
{
    Task<List<StandingDTO>> GetStandingsAsync();

    Task<List<TopScorerDTO>> GetTopScorersAsync();

    Task<List<CardStatsDTO>> GetCardStatsAsync();
}