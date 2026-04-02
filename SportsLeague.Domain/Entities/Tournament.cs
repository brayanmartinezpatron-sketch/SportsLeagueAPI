namespace SportsLeague.Domain.Entities;

public class Tournament : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }

    public string Status { get; set; } = "Pending";

    public List<Team> Teams { get; set; } = new();
}