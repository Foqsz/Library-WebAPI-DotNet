using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Application.Services;
using BibliotecaApi.Library.Infrastructure.Data;
using BibliotecaApi.Library.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiCatalogo.Catalogo.Core.Model;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentity<LibraryUser, IdentityRole>()
    .AddEntityFrameworkStores<LibraryContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BibliotecaApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
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
                }
            },
            new string[] {}
        }
    });
}); 

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BibliotecaApi v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/test", () => "OK!").RequireAuthorization();

app.MapControllers();

app.Run();
