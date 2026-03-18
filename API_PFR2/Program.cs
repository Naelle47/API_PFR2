using System.Text;
using API_PFR2.BLL;
using API_PFR2.DAL;
using API_PFR2.Domain.Enums;
using API_PFR2.Presentation.API_REST.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

NpgsqlConnection.GlobalTypeMapper.MapEnum<StatutInscription>("statut_inscription");

// ---------------------------
// Configuration files
// ---------------------------
builder.Configuration
    .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true);


// ---------------------------
// Controllers
// ---------------------------
builder.Services.AddControllers(options =>
{
#if !DEBUG
    options.Filters.Add(typeof(APIExceptionFilterAttribute));
#endif
});


// ----------------------------------------
// DAL & BLL -- IOC & Dependency Injection
// ----------------------------------------
builder.Services.AddDAL(builder.Configuration);
builder.Services.AddBLL();


// ---------------------------
// JWT Authentication
// ---------------------------
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,

            ValidIssuer = builder.Configuration["JWTIssuer"],
            ValidAudience = builder.Configuration["JWTAudience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWTSecret"]!)
            ),

            ClockSkew = TimeSpan.Zero
        };
    });


// ---------------------------
// Swagger / OpenAPI
// ---------------------------
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API PFR2",
        Version = "v1",
        Description = "API for the board game bar management system."
    });

    // XML documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // JWT support in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


// ---------------------------
// Build app
// ---------------------------
var app = builder.Build();


// ---------------------------
// Middleware pipeline
// ---------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// ------------------------------
// Integration Tests entry point
// ------------------------------
 public partial class Program { }