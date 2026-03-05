using System.Data;
using Npgsql;

namespace API_PFR2.DAL;

/// <summary>
/// 
/// </summary>
public static class DALExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        // Récupération de la chaîne de connexion depuis appsettings.json
        string connectionString = configuration.GetConnectionString("GestionCatalogue")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        // Injection de la connexion Dapper / PostgreSQL
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));

        // Injection des repositories
        //services.AddScoped<IJeuRepository, JeuRepository>();
        //services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

        return services;
    }

}
