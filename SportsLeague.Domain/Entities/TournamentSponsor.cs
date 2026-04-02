namespace SportsLeague.Domain.Entities;

public class TournamentSponsor
{
    public int Id { get; set; }

    public int SponsorId { get; set; }
    public Sponsor Sponsor { get; set; }

    public int TournamentId { get; set; }
    public Tournament Tournament { get; set; }
    public decimal ContractAmount { get; set; }
}