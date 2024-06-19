using BibliotecaApi.Library.Application.DTOs.Mappings;
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


var origensComAcessoPermitido = "_origensComAcessoPermitido";

builder.Services.AddCors(options =>
    options.AddPolicy(name: origensComAcessoPermitido,
    policy =>
    {
        policy.WithOrigins("https://apirequest.io");
    })
);

var secretKey = builder.Configuration["JWT:SecretKey"]
                    ?? throw new ArgumentException("Erro! Invalid secret key!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("SuperAdmin", policy => policy.RequireRole("Admin").RequireClaim("id", "Foqs"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    options.AddPolicy("ExclusivePolicyOnly", policy =>
                      policy.RequireAssertion(context =>
                      context.User.HasClaim(claim =>
                      claim.Type == "id" && claim.Value == "Foqs") || context.User.IsInRole("SuperAdmin")));

});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(UsuarioDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(LivroDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(UserModelDTOMappingProfile));

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

app.UseCors(origensComAcessoPermitido);

app.UseAuthentication();
app.UseAuthorization();
 

app.MapControllers();

app.Run();
