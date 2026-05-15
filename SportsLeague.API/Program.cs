using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.DataAccess.Seeders;
using SportsLeague.API.Interfaces;
using SportsLeague.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LeagueDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=SportsLeagueDb;Trusted_Connection=true;TrustServerCertificate=true;"));

builder.Services.AddControllers();
builder.Services.AddScoped<IStandingsService, StandingsService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MatchLineupService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LeagueDbContext>();

    await context.Database.MigrateAsync();
    await DataSeeder.SeedAsync(context);
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
