
using API_PFR2.BLL;
using API_PFR2.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
#if !DEBUG
    options.Filters.Add(typeof(ApiExceptionFilterAttribute));
#endif
});
builder.Services.AddDAL(builder.Configuration);
builder.Services.AddBLL();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();


// public partial class Program { } --  à décommenter plus tard pour les tests d'intégration avec WebApplicationFactory<Program> dans le projet de tests.
// TODO : Télécharger le package NuGet Microsoft.AspNetCore.Mvc.Testing pour les tests d'intégration.
// TODO : Ajouter une classe de test d'intégration APIWebApplicationFactory.cs dans un projet de tests séparé, en utilisant WebApplicationFactory<Program> pour tester les endpoints de l'API.
// -- APIWebApplicationFactory.cs hérite de WebApplicationFactory<Program> et configure le serveur de test pour les tests d'intégration.
// Ce qui donne : APIWebApplicationFactory : WebApplicationFactory<Program>