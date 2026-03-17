using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
namespace API_PFR2_TestsIntegration.Fixtures;

public class APIWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration? Configuration { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(
                    Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Integration.json"))
                .Build();
            config.AddConfiguration(Configuration);
        });
    }
}