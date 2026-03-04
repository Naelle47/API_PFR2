using System.Data;
using Npgsql;

namespace API_PFR2.DAL;

public static class DALExtensions
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        // Récupération de la chaîne de connexion depuis appsettings.json
        string connectionString = configuration.GetConnectionString("GestionCatalogue")
            ;


        // Injection de la connexion Dapper / PostgreSQL
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));

        // Injection des repositories
        //services.AddScoped<IJeuRepository, JeuRepository>();
        //services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

        return services;
    }

}
