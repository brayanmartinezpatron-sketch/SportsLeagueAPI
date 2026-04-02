namespace SportsLeague.Domain.Entities;

public class Coach : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;
}