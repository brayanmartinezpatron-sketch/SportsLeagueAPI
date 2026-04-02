using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Entities;

public class Player : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Position { get; set; } = string.Empty;

    public int TeamId { get; set; }
    public Team Team { get; set; }
}