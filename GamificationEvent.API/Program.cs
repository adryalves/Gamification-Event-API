using GamificationEvent.Application.UseCases.EventoUseCases;
using GamificationEvent.Application.UseCases.PaletaCorUseCases;
using GamificationEvent.Application.UseCases.UsuarioUseCases;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using GamificationEvent.Infrastructure.Repositories;
using GamificationEvent.Infrastructure.Serviços;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddControllers();




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
