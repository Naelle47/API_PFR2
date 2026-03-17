using System.Net.Http.Headers;
using System.Net.Http.Json;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace API_PFR2_TestsIntegration.Fixtures;

public abstract class AbstractIntegrationTest : IClassFixture<APIWebApplicationFactory>
{
    protected HttpClient _client;

    protected AbstractIntegrationTest(APIWebApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
    }

    public void LogOut()
    {
        _client.DefaultRequestHeaders.Authorization = null;
    }

    public async Task LogIn(string email, string password)
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email = email,
            Password = password
        });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", result!.Token);
        }
        else
        {
            throw new Exception($"Login failed for {email}");
        }
    }
}