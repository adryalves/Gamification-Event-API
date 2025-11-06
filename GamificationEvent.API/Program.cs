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
using GamificationEvent.Application.UseCases.QuizParticipanteUseCases;
using GamificationEvent.Application.UseCases.QuizUseCases;
using GamificationEvent.Application.UseCases.RankingUseCases;
using GamificationEvent.Application.UseCases.SubEventoUseCases;
using GamificationEvent.Application.UseCases.UsuarioUseCases;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Validações;
using GamificationEvent.Infrastructure.Data.Persistence;
using GamificationEvent.Infrastructure.Repositories;
using GamificationEvent.Infrastructure.Serviços;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

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

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretKey"])),
        ClockSkew = TimeSpan.Zero

    };
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
builder.Services.AddScoped<IQuizParticipanteRepository, QuizParticipanteRepository>();



// Servi�os Infra
builder.Services.AddScoped<ISenhaHash, SenhaHash>();
builder.Services.AddScoped<IQrCode, QrCode>();
builder.Services.AddScoped<IAuthenticate, Authenticate>();
builder.Services.AddScoped<IValidaçãoPermissões, ValidaçãoPermissões>();


// UseCase
builder.Services.AddScoped<CadastrarUsuarioUseCase>();
builder.Services.AddScoped<GetUsuariosUseCase>();
builder.Services.AddScoped<GetUsuarioPorIdUseCase>();
builder.Services.AddScoped<DeletarUsuarioUseCase>();
builder.Services.AddScoped<AtualizarUsuarioUseCase>();
builder.Services.AddScoped<UsuarioLoginUseCase>();

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

builder.Services.AddScoped<CadastrarParticipanteQuizRespostaUseCase>();
builder.Services.AddScoped<CadastrarQuizParticipanteUseCase>();
builder.Services.AddScoped<GetParticipantesQuizPorIdQuizUseCase>();
builder.Services.AddScoped<GetQuizzesPorIdParticipanteUseCase>();
builder.Services.AddScoped<GetResultadoParticipanteQuizUseCase>();
builder.Services.AddScoped<GetQuizRankingUseCase>();
builder.Services.AddScoped<DeletarTodasAsRespostasDoQuizUseCase>();




builder.Services.AddControllers() .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });;



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GamificationEvent.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer.Digite **'Bearer' [espa�o] seu token** no campo abaixo.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
var app = builder.Build();


var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

// Habilita Swagger tanto em dev quanto em produ��o (Render)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS redirection pode causar conflito em Render (n�o h� certificado interno)
// Ent�o, s� use se estiver local
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapGet("/", () => Results.Ok("API GamificationEvent est� rodando"));

app.Run();