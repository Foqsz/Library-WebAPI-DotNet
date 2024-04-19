using Microsoft.EntityFrameworkCore; 
using BibliotecaApi.Models;
using BibliotecaApi.Repositorios.Interfaces; 
using BibliotecaApi.Repository;
using BibliotecaApi.Services.Interfaces;
using BibliotecaApi.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Carregar a configura��o do arquivo appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Adicionar servi�os ao cont�iner.
builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ConexaoPadrao")));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); 

// Configure o pipeline de solicita��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
