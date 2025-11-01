using GamificationEvent.API;
using GamificationEvent.Application.UseCases.CheckInSubEventoCases;
using GamificationEvent.Application.UseCases.DesafioUseCases;
using GamificationEvent.Application.UseCases.EventoUseCases;
using GamificationEvent.Application.UseCases.InscritoUseCases;
using GamificationEvent.Application.UseCases.InteresseUseCases;
using GamificationEvent.Application.UseCases.PalestranteUseCases;
using GamificationEvent.Application.UseCases.PaletaCorUseCases;
using GamificationEvent.Application.UseCases.ParticipantePremioUseCases;
using GamificationEvent.Application.UseCases.ParticipanteUseCases;
using GamificationEvent.Application.UseCases.PremioUseCases;
using GamificationEvent.Application.UseCases.QuizUseCases;
using GamificationEvent.Application.UseCases.RankingUseCases;
using GamificationEvent.Application.UseCases.SubEventoUseCases;
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
builder.Services.AddScoped<IPremioRepository, PremioRepository>();
builder.Services.AddScoped<IParticipantePremioRepository, ParticipantePremioRepository>();
builder.Services.AddScoped<IPalestranteRepository, PalestranteRepository>();
builder.Services.AddScoped<ISubEventoRepository, SubEventoRepository>();
builder.Services.AddScoped<IDesafioRepository, DesafioRepository>();
builder.Services.AddScoped<ICheckInSubEventoRepository, CheckInSubEventoRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();


// Serviços Infra
builder.Services.AddScoped<ISenhaHash, SenhaHash>();
builder.Services.AddScoped<IQrCode, QrCode>();


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
builder.Services.AddScoped<GetParticipantePorCpfUseCase>();


builder.Services.AddScoped<GetRankingGeralPorIdEventoUseCase>();
builder.Services.AddScoped<GetRankingPersonalizadoUseCase>();

builder.Services.AddScoped<AtualizarPremioUseCase>();
builder.Services.AddScoped<CadastrarPremioUseCase>();
builder.Services.AddScoped<DeletarPremioUseCase>();
builder.Services.AddScoped<GetPremioPorIdUseCase>();
builder.Services.AddScoped<GetPremiosPorIdEventoUseCase>();

builder.Services.AddScoped<AtualizarParticipantePremioUseCase>();
builder.Services.AddScoped<CadastrarParticipantePremioUseCase>();
builder.Services.AddScoped<GetParticipantePremioPorIdUseCase>();
builder.Services.AddScoped<GetParticipantePremiosPorIdEventoUseCase>();
builder.Services.AddScoped<GetParticipantePremiosPorIdParticipanteUseCase>();
builder.Services.AddScoped<GetParticipantesPremioPorIdPremioUseCase>();

builder.Services.AddScoped<AtualizarPalestranteUseCase>();
builder.Services.AddScoped<CadastrarPalestranteUseCase>();
builder.Services.AddScoped<DeletarPalestranteUseCase>();
builder.Services.AddScoped<GetPalestrantePorIdUseCase>();
builder.Services.AddScoped<GetPalestrantesPorIdEventoUseCase>();
builder.Services.AddScoped<GetPalestrantesPorIdSubEventoUseCase>();

builder.Services.AddScoped<AdicionarPerguntaProSubEventoUseCase>();
builder.Services.AddScoped<AtualizarSubEventoUseCase>();
builder.Services.AddScoped<CadastrarSubEventoUseCase>();
builder.Services.AddScoped<DeletarSubEventoUseCase>();
builder.Services.AddScoped<GetPerguntasPorIdSubEventoUseCase>();
builder.Services.AddScoped<GetSubEventoPorIdUseCase>();
builder.Services.AddScoped<GetSubEventosPorIdEventoUseCase>();

builder.Services.AddScoped<AtualizarDesafioUseCase>();
builder.Services.AddScoped<CadastrarDesafioUseCase>();
builder.Services.AddScoped<DeletarDesafioUseCase>();
builder.Services.AddScoped<GetDesafioPorIdUseCase>();
builder.Services.AddScoped<GetDesafiosPorIdEventoUseCase>();
builder.Services.AddScoped<GetDesafiosParticipantePorIdParticipanteUseCase>();

builder.Services.AddScoped<CadastrarCheckInSubEventoUseCase>();
builder.Services.AddScoped<GerarQrCodeUseCaseSubEvento>();
builder.Services.AddScoped<GetCheckInsSubEventoPorIdParticipanteUseCase>();
builder.Services.AddScoped<GetCheckInsSubEventoPorIdSubEventoUseCase>();
builder.Services.AddScoped<GetCheckInSubEventoPorIdUseCase>();


builder.Services.AddScoped<AdicionarAlternativasQuizUseCase>();
builder.Services.AddScoped<AdicionarPerguntaQuizUseCase>();
builder.Services.AddScoped<AtualizarQuizPerguntaUseCase>();
builder.Services.AddScoped<AtualizarQuizUseCase>();
builder.Services.AddScoped<CadastrarQuizUseCase>();
builder.Services.AddScoped<DeletarAlternativaQuizUseCase>();
builder.Services.AddScoped<DeletarPerguntaQuizUseCase>();
builder.Services.AddScoped<DeletarQuizUseCase>();
builder.Services.AddScoped<GetQuizPorIdUseCase>();
builder.Services.AddScoped<GetQuizzesPorIdEventoUseCase>();
builder.Services.AddScoped<GetTodasAsPerguntasPorIdQuizUseCase>();



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
