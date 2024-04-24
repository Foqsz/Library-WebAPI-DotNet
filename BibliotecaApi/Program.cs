using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using BibliotecaApi.Configuration;
using BibliotecaApi.Models;
using BibliotecaApi.Repository;
using BibliotecaApi.Repositorios.Interfaces;
using BibliotecaApi.Services;
using BibliotecaApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.Text;
using BibliotecaApi.Model;
using BibliotecaApi.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Carregar a configuração do arquivo appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ConexaoPadrao")));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddTransient<AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração da autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthConfiguration.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("tech", policy =>
    {
        policy.RequireRole("developer");
    });
});

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 


app.MapGet("/test", () => "OK!").RequireAuthorization();

app.MapControllers();

app.Run();
