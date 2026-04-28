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
    public DbSet<TournamentTeam> TournamentTeams => Set<TournamentTeam>();

    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchResult> MatchResults => Set<MatchResult>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<Card> Cards => Set<Card>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      
        modelBuilder.Entity<TournamentTeam>()
            .HasKey(tt => new { tt.TournamentId, tt.TeamId });

        
        modelBuilder.Entity<MatchResult>()
            .HasOne(mr => mr.Match)
            .WithOne(m => m.MatchResult)
            .HasForeignKey<MatchResult>(mr => mr.MatchId);

        modelBuilder.Entity<MatchResult>()
            .HasIndex(mr => mr.MatchId)
            .IsUnique();

        modelBuilder.Entity<Match>()
    .HasOne(m => m.HomeTeam)
    .WithMany()
    .HasForeignKey(m => m.HomeTeamId)
    .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany()
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}