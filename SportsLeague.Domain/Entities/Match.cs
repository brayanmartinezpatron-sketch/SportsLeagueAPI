using System.Text.Json.Serialization;

namespace SportsLeague.Domain.Entities;

public class Match : AuditBase
{
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }

    public DateTime MatchDate { get; set; }

    [JsonIgnore]
    public Team? HomeTeam { get; set; }

    [JsonIgnore]
    public Team? AwayTeam { get; set; }

    public MatchResult? MatchResult { get; set; }

    public List<Goal> Goals { get; set; } = new();

    public List<Card> Cards { get; set; } = new();
}