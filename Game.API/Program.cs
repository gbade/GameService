using Game.Application.Interfaces.Repository;
using Game.Application.Interfaces.Services;
using Game.Core.Services;
using Game.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IScoreboardService, ScoreboardService>();

//Register repositories
builder.Services.AddSingleton<IGameRepository, GameRepository>();
builder.Services.AddSingleton<IScoreboardRepository, ScoreboardRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(cpb => {
    cpb.AllowAnyOrigin();
    cpb.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
