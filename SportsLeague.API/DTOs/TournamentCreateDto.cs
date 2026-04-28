namespace SportsLeague.API.DTOs;

public class TournamentCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public decimal Prize { get; set; }
    public string Status { get; set; } = string.Empty;
}