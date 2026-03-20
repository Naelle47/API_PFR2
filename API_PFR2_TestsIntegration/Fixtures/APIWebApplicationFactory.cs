using System.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Npgsql;
using API_PFR2.Domain.Enums;
namespace API_PFR2_TestsIntegration.Fixtures;

public class APIWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration? Configuration { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(
                    Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Integration.json"))
                .Build();
            config.AddConfiguration(Configuration);
        });

        builder.ConfigureServices(services =>
        {
            // Supprimer NpgsqlDataSource existant
            var dataSourceDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(NpgsqlDataSource));
            if (dataSourceDescriptor != null)
                services.Remove(dataSourceDescriptor);

            // Supprimer IDbConnection existant
            var dbDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IDbConnection));
            if (dbDescriptor != null)
                services.Remove(dbDescriptor);

            // Recréer avec la base de test + mapping des enums
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(
                "Host=localhost;Database=api_pfr2_test;Username=postgres;Password=password;Port=5432");
            dataSourceBuilder.MapEnum<RoleUtilisateur>("role_utilisateur");
            dataSourceBuilder.MapEnum<StatutInscription>("statut_inscription");
            var dataSource = dataSourceBuilder.Build();

            services.AddSingleton(dataSource);
            services.AddScoped<IDbConnection>(sp => dataSource.CreateConnection());
        });
    }
}