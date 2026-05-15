namespace SportsLeague.API.DTOs;

public class CreateMatchLineupDto
{
    public int PlayerId { get; set; }

    public bool IsStarter { get; set; }

    public string Position { get; set; } = string.Empty;
}