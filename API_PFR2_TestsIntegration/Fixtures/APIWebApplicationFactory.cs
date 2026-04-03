using System.Data;
using API_PFR2.Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class APIWebApplicationFactory
    : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string TestConnectionString =
        "Host=localhost;Database=api_pfr2_test;Username=postgres;Password=password;Port=5432";

    public IConfiguration? Configuration { get; set; }

    public async Task InitializeAsync()
    {
        var basePath = AppContext.BaseDirectory;

        await ExecuteSqlFile(Path.Combine(basePath, "Data", "cleanup.sql"));
        await ExecuteSqlFile(Path.Combine(basePath, "Data", "seed.sql"));
    }

    public Task DisposeAsync() => Task.CompletedTask;

    private async Task ExecuteSqlFile(string path)
    {
        var sql = await File.ReadAllTextAsync(path);

        await using var conn = new NpgsqlConnection(TestConnectionString);
        await conn.OpenAsync();

        var commands = sql.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var cmdText in commands)
        {
            var trimmed = cmdText.Trim();
            if (string.IsNullOrWhiteSpace(trimmed)) continue;

            await using var cmd = new NpgsqlCommand(trimmed, conn);
            await cmd.ExecuteNonQueryAsync();
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Integration");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.Integrations.json"))
                .Build();

            config.AddConfiguration(Configuration);
        });

        builder.ConfigureServices(services =>
        {
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
}