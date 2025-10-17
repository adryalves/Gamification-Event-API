using GamificationEvent.API;
using GamificationEvent.Application.UseCases.EventoUseCases;
using GamificationEvent.Application.UseCases.InscritoUseCases;
using GamificationEvent.Application.UseCases.InteresseUseCases;
using GamificationEvent.Application.UseCases.PaletaCorUseCases;
using GamificationEvent.Application.UseCases.ParticipanteUseCases;
using GamificationEvent.Application.UseCases.RankingUseCases;
using GamificationEvent.Application.UseCases.UsuarioUseCases;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using GamificationEvent.Infrastructure.Repositories;
using GamificationEvent.Infrastructure.Serviços;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString), 
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
    );
});

// Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPaletaCorRepository, PaletaCorRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IInscritoRepository, InscritoRepository>();
builder.Services.AddScoped<IInteresseRepository, InteresseRepository>();
builder.Services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
builder.Services.AddScoped<IRankingRepository, RankingRepository>();

// Serviços Infra
builder.Services.AddScoped<ISenhaHash, SenhaHash>();


// UseCase
builder.Services.AddScoped<CadastrarUsuarioUseCase>();
builder.Services.AddScoped<GetUsuariosUseCase>();
builder.Services.AddScoped<GetUsuarioPorIdUseCase>();
builder.Services.AddScoped<DeletarUsuarioUseCase>();
builder.Services.AddScoped<AtualizarUsuarioUseCase>();

builder.Services.AddScoped<AtualizarCorUseCase>();
builder.Services.AddScoped<AtualizarPaletaUseCase>();
builder.Services.AddScoped<CadastrarCorUseCase>();
builder.Services.AddScoped<CadastrarPaletaUseCase>();
builder.Services.AddScoped<DeletarPaletaUseCase>();
builder.Services.AddScoped<GetCoresUseCase>();
builder.Services.AddScoped<GetCorPorIdUseCase>();
builder.Services.AddScoped<GetPaletaPorIdUseCase>();
builder.Services.AddScoped<GetPaletasUseCase>();

builder.Services.AddScoped<AtualizarEventoUseCase>();
builder.Services.AddScoped<CadastrarEventoUseCase>();
builder.Services.AddScoped<DeletarEventoUseCase>();
builder.Services.AddScoped<GetEventoPorIdUseCase>();
builder.Services.AddScoped<GetEventosUseCase>();

builder.Services.AddScoped<CadastrarInscritosUseCase>();
builder.Services.AddScoped<CadastrarInscritoUseCase>();
builder.Services.AddScoped<DeletarInscritoUseCase>();
builder.Services.AddScoped<GetInscritosPorIdUseCase>();
builder.Services.AddScoped<GetInscritosUseCase>();

builder.Services.AddScoped<CadastrarInteresseUseCase>();
builder.Services.AddScoped<DeletarInteresseUseCase>();
builder.Services.AddScoped<GetInteressePorIdUseCase>();
builder.Services.AddScoped<GetInteressesPorIdEventoUseCase>();

builder.Services.AddScoped<AtualizarParticipanteUseCase>();
builder.Services.AddScoped<CadastrarParticipanteUseCase>();
builder.Services.AddScoped<GetParticipantePorIdUseCase>();
builder.Services.AddScoped<GetParticipantesPorIdEventoUseCase>();


builder.Services.AddScoped<GetRankingGeralPorIdEventoUseCase>();
builder.Services.AddScoped<GetRankingPersonalizadoUseCase>();


builder.Services.AddControllers() .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });;


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

app.UseAuthorization();

app.MapControllers();

app.Run();
