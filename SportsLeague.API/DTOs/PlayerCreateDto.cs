namespace SportsLeague.API.DTOs;

public class PlayerCreateDto
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Position { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public DateTime DateOfBirth { get; set; }
}