using System.Net.Http.Json;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using API_PFR2_TestsIntegration.Fixtures;
namespace API_PFR2_TestsIntegration;

public class AuthControllerTests : AbstractIntegrationTest
{
    public AuthControllerTests(APIWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "user1@nivo.fr",
            Password = "pasteque"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(result);
        Assert.NotEmpty(result.Token);
    }

    [Fact]
    public async Task Login_ShouldReturn401_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "wrong@test.com",
            Password = "wrongpassword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}