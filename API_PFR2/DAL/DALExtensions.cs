using System.Data;
using API_PFR2.DAL.Implementations;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Enums;
using Npgsql;

namespace API_PFR2.DAL;

/// <summary>
/// Provides extension methods for registering Data Access Layer services
/// in the dependency injection container.
/// </summary>
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
        // Retrieve the connection string from configuration
        string connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // -------------------------------
        // Create NpgsqlDataSource and map enums
        // -------------------------------
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

        // Map PostgreSQL enums to C# enums
        dataSourceBuilder.MapEnum<RoleUtilisateur>("role_utilisateur");
        dataSourceBuilder.MapEnum<StatutInscription>("statut_inscription");

        var dataSource = dataSourceBuilder.Build();

        // Inject the DataSource as singleton
        services.AddSingleton(dataSource);

        // Inject IDbConnection scoped per request
        services.AddScoped<IDbConnection>(sp => dataSource.CreateConnection());

        // -------------------------------
        // Register repositories
        // -------------------------------
        services.AddScoped<IJeuRepository, JeuRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}