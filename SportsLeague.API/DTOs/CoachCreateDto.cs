namespace SportsLeague.API.DTOs;

public class CoachCreateDto
{
    public string Name { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public int TeamId { get; set; }
}