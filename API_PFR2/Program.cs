
using System.Text;
using API_PFR2.BLL;
using API_PFR2.DAL;
using API_PFR2.Presentation.API_REST.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
#if !DEBUG
    options.Filters.Add(typeof(ApiExceptionFilterAttribute));
#endif
});

// Ajout des services de la couche DAL (Data Access Layer) avec la configuration de l'application
builder.Services.AddDAL(builder.Configuration);

// Ajout des services de la couche BLL (Business Logic Layer)
builder.Services.AddBLL();

// JWT Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration.GetValue<string>("JWTIssuer"),
            ValidAudience = builder.Configuration.GetValue<string>("JWTAudience"),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"))),
            ClockSkew = TimeSpan.Zero

        };

    });

// -- Swagger pour JWT Authentication pour permettre de tester les endpoints protégés par JWT directement depuis l'interface Swagger UI.
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


// Builder pour Swagger/OpenAPI
// Ajout de Swagger pour la documentation de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API PFR2",
        Version = "v1",
        Description = "API for the board game bar management system."
    });

    // Activation des commentaires XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();


// public partial class Program { } --  à décommenter plus tard pour les tests d'intégration avec WebApplicationFactory<Program> dans le projet de tests.
// TODO : Télécharger le package NuGet Microsoft.AspNetCore.Mvc.Testing pour les tests d'intégration.
// TODO : Ajouter une classe de test d'intégration APIWebApplicationFactory.cs dans un projet de tests séparé, en utilisant WebApplicationFactory<Program> pour tester les endpoints de l'API.
// -- APIWebApplicationFactory.cs hérite de WebApplicationFactory<Program> et configure le serveur de test pour les tests d'intégration.
// Ce qui donne : APIWebApplicationFactory : WebApplicationFactory<Program>