using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BibliotecaApi.Models;
using BibliotecaApi.Repository;
using BibliotecaApi.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Carregar a configuração do arquivo appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ConexaoPadrao")));
builder.Services.AddScoped<IUsuarioServiceDTO, UsuarioRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
