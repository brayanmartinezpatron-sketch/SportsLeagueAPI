using System.Text.Json.Serialization;

namespace SportsLeague.Domain.Entities;

public class Player : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Position { get; set; } = string.Empty;

    public int TeamId { get; set; }

    [JsonIgnore] 
    public Team Team { get; set; }

    public ICollection<Goal> Goals { get; set; } = new List<Goal>();

    public ICollection<Card> Cards { get; set; } = new List<Card>();
}