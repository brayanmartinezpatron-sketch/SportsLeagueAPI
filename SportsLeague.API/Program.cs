using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LeagueDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=SportsLeagueDb;Trusted_Connection=true;TrustServerCertificate=true;"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
