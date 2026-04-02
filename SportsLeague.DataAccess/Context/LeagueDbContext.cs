using Microsoft.EntityFrameworkCore;
using SportsLeague.Domain.Entities;

namespace SportsLeague.DataAccess.Context;

public class LeagueDbContext : DbContext
{
    public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
        : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Coach> Coaches => Set<Coach>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();
    public DbSet<Referee> Referees => Set<Referee>();
    public DbSet<Sponsor> Sponsors => Set<Sponsor>();
    public DbSet<TournamentSponsor> TournamentSponsors => Set<TournamentSponsor>();
}