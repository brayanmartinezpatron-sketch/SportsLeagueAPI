using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SportsLeague.DataAccess.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(LeagueDbContext context)
    {

        // Equipos
        var teams = new List<Team>
{
    new() { Name="Atlético Nacional", City="Medellín", Stadium="Atanasio Girardot" },
    new() { Name="Independiente Medellín", City="Medellín", Stadium="Atanasio Girardot" },
    new() { Name="América de Cali", City="Cali", Stadium="Pascual Guerrero" },
    new() { Name="Deportivo Cali", City="Cali", Stadium="Deportivo Cali" },
    new() { Name="Junior FC", City="Barranquilla", Stadium="Metropolitano" },
    new() { Name="Millonarios FC", City="Bogotá", Stadium="El Campín" },
    new() { Name="Independiente Santa Fe", City="Bogotá", Stadium="El Campín" },
    new() { Name="Deportes Tolima", City="Ibagué", Stadium="Manuel Murillo Toro" },
    new() { Name="Atlético Bucaramanga", City="Bucaramanga", Stadium="Alfonso López" },
    new() { Name="Once Caldas", City="Manizales", Stadium="Palogrande" },
    new() { Name="Deportivo Pasto", City="Pasto", Stadium="Departamental Libertad" },
    new() { Name="Deportivo Pereira", City="Pereira", Stadium="Hernán Ramírez Villegas" },
    new() { Name="Águilas Doradas", City="Rionegro", Stadium="Alberto Grisales" },
    new() { Name="Boyacá Chicó FC", City="Tunja", Stadium="La Independencia" },
    new() { Name="Jaguares de Córdoba", City="Montería", Stadium="Jaraguay" },
    new() { Name="Alianza Valledupar FC", City="Valledupar", Stadium="Armando Maestre" },
    new() { Name="Fortaleza FC", City="Bogotá", Stadium="Metropolitano de Techo" },
    new() { Name="Llaneros FC", City="Villavicencio", Stadium="Bello Horizonte" },
    new() { Name="Cúcuta Deportivo", City="Cúcuta", Stadium="General Santander" },
    new() { Name="Internacional de Bogotá", City="Bogotá", Stadium="Metropolitano de Techo" }
};

        context.Teams.AddRange(teams);
        await context.SaveChangesAsync();

        // Jugadores
        var players = new List<Player>();

        foreach (var team in teams)
        {
            for (int i = 1; i <= 5; i++)
            {
                players.Add(new Player
                {
                    Name = $"Jugador {i} {team.Name}",
                    Age = 20 + i,
                    Position = "Delantero",
                    DateOfBirth = DateTime.Now.AddYears(-20 - i),
                    TeamId = team.Id
                });
            }
        }

        context.Players.AddRange(players);
        await context.SaveChangesAsync();

        // Árbitros
        var referees = new List<Referee>
{
    new() { FirstName = "Carlos", LastName = "Ortega", Nationality = "Colombia" },
    new() { FirstName = "Wilmar", LastName = "Roldán", Nationality = "Colombia" },
    new() { FirstName = "Andrés", LastName = "Rojas", Nationality = "Colombia" },
    new() { FirstName = "Nicolás", LastName = "Gallo", Nationality = "Colombia" }
};

        context.Referees.AddRange(referees);
        await context.SaveChangesAsync();

        // Torneo
        var tournament = new Tournament
        {
            Name = "Liga Colombiana",
            StartDate = DateTime.Now
        };

        context.Tournaments.Add(tournament);
        await context.SaveChangesAsync();

        // Inscribir equipos al torneo
        var tournamentTeams = new List<TournamentTeam>();

        foreach (var team in teams)
        {
            tournamentTeams.Add(new TournamentTeam
            {
                TeamId = team.Id,
                TournamentId = tournament.Id
            });
        }

        context.TournamentTeams.AddRange(tournamentTeams);
        await context.SaveChangesAsync();


    }
}