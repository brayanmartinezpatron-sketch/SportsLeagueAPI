using System.Text.Json.Serialization;

namespace SportsLeague.Domain.Entities;

public class Card : AuditBase
{
    public int MatchId { get; set; }
    public int PlayerId { get; set; }

    public int Minute { get; set; }

    public int Type { get; set; }

    [JsonIgnore]
    public Match? Match { get; set; }

    [JsonIgnore]
    public Player? Player { get; set; }
}