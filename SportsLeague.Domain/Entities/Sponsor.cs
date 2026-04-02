namespace SportsLeague.Domain.Entities;

public class Sponsor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? WebsiteUrl { get; set; }

    public List<TournamentSponsor> TournamentSponsors { get; set; } = new();
}