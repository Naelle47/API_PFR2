using System.Data;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace API_PFR2_TestsIntegration.Fixtures;

public class APIWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration? Configuration { get; set; }

    public APIWebApplicationFactory() : base()
    {
        // Seed the test database
        using var connection = new NpgsqlConnection(
            "Host=localhost;Database=api_pfr2_test;Username=postgres;Password=password;Port=5432");
        connection.Open();

        // 1. Exécuter cleanup pour supprimer les anciennes tables
        var cleanupSql = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data", "cleanup.sql"));
        using (var cleanupCommand = new NpgsqlCommand(cleanupSql, connection))
        {
            cleanupCommand.ExecuteNonQuery();
        }

        // 2. Seed
        var sql = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data", "seed.sql"));

        var commands = Regex.Split(sql, @";\s*(\r?\n|$)", RegexOptions.Multiline);
        foreach (var commandText in commands)
        { 
            var trimmed = commandText.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
                continue;

            using var command = new NpgsqlCommand(trimmed, connection);
            command.ExecuteNonQuery();

        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        //appsettings.integration.json
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Integrations.json")
                .Build();
            config.AddConfiguration(Configuration);

        });

    }

    protected override void Dispose(bool disposing)
    {
        // Cleanup the test database
        using var connection = new NpgsqlConnection(
            "Host=localhost;Database=api_pfr2_test;Username=postgres;Password=password;Port=5432");
        connection.Open();
        var sql = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data", "cleanup.sql"));
        using var command = new NpgsqlCommand(sql, connection);
        command.ExecuteNonQuery();
        base.Dispose(disposing);
    }
}