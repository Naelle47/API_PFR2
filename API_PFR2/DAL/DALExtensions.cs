using System.Data;
using API_PFR2.DAL.Implementations;
using API_PFR2.DAL.Interfaces;
using Npgsql;

namespace API_PFR2.DAL;

/// <summary>
/// Provides extension methods for registering Data Access Layer services
/// in the dependency injection container.
/// </summary>
/// <remarks>
/// This class centralizes the configuration of repositories and database
/// connections used by the application.
/// </remarks>
public static class DALExtensions
{
    /// <summary>
    /// Registers the Data Access Layer services in the dependency injection container.
    /// </summary>
    /// <param name="services">
    /// The service collection used to register application services.
    /// </param>
    /// <param name="configuration">
    /// The application configuration used to retrieve the database connection string.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with DAL services registered.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required database connection string is not found in the configuration.
    /// </exception>
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        // Récupération de la chaîne de connexion depuis appsettings.json
        string connectionString = configuration.GetConnectionString("GestionCatalogue")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        // Injection de la connexion Dapper / PostgreSQL
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));

        // Injection des repositories
        services.AddScoped<IJeuRepository, JeuRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        //services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

        return services;
    }

}
