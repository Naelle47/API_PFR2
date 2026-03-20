using System.Data;
using System.Text.RegularExpressions;
using API_PFR2.Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Npgsql;
namespace API_PFR2_TestsIntegration.Fixtures;

public class APIWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string TestConnectionString =
        "Host=localhost;Database=api_pfr2_test;Username=postgres;Password=password;Port=5432";

    public IConfiguration? Configuration { get; set; }

    public APIWebApplicationFactory() : base()
    {
        using var connection = new NpgsqlConnection(TestConnectionString);
        connection.Open();

        // 1. Cleanup
        var cleanupSql = File.ReadAllText(
            Path.Combine(Directory.GetCurrentDirectory(), "Data", "cleanup.sql"));
        using (var cleanupCommand = new NpgsqlCommand(cleanupSql, connection))
        {
            cleanupCommand.ExecuteNonQuery();
        }

        // 2. Seed
        var sql = File.ReadAllText(
            Path.Combine(Directory.GetCurrentDirectory(), "Data", "seed.sql"));
        using var command = new NpgsqlCommand(sql, connection);
        command.ExecuteNonQuery();

        //var sql = File.ReadAllText(
        //    Path.Combine(Directory.GetCurrentDirectory(), "Data", "seed.sql"));
        //var commands = Regex.Split(sql, @";\s*(\r?\n|$)", RegexOptions.Multiline);
        //foreach (var commandText in commands)
        //{
        //    var trimmed = commandText.Trim();
        //    if (string.IsNullOrWhiteSpace(trimmed))
        //        continue;
        //    using var command = new NpgsqlCommand(trimmed, connection);
        //    command.ExecuteNonQuery();
        //}
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(
                    Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Integrations.json"))
                .Build();
            config.AddConfiguration(Configuration);
        });

        builder.ConfigureServices(services =>
        {
            // Remplacer NpgsqlDataSource par celle de la base de test
            var dataSourceDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(NpgsqlDataSource));
            if (dataSourceDescriptor != null)
                services.Remove(dataSourceDescriptor);

            var dbDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IDbConnection));
            if (dbDescriptor != null)
                services.Remove(dbDescriptor);

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(TestConnectionString);
            dataSourceBuilder.MapEnum<RoleUtilisateur>("role_utilisateur");
            dataSourceBuilder.MapEnum<StatutInscription>("statut_inscription");
            var dataSource = dataSourceBuilder.Build();

            services.AddSingleton(dataSource);
            services.AddScoped<IDbConnection>(sp => dataSource.CreateConnection());
        });
    }

    protected override void Dispose(bool disposing)
    {
        using var connection = new NpgsqlConnection(TestConnectionString);
        connection.Open();
        var sql = File.ReadAllText(
            Path.Combine(Directory.GetCurrentDirectory(), "Data", "cleanup.sql"));
        using var command = new NpgsqlCommand(sql, connection);
        command.ExecuteNonQuery();
        base.Dispose(disposing);
    }
}