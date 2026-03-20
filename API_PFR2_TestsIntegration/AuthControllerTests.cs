using System.Net.Http.Json;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using API_PFR2_TestsIntegration.Fixtures;

namespace API_PFR2_TestsIntegration;

public class AuthControllerTests : AbstractIntegrationTest
{
    // Note : Although this class inherits from AbstractIntegrationTest,
    // the LogIn method is not called here since we are testing the authentication
    // endpoint itself. LogIn will be used in other controller tests that require
    // an authenticated user (e.g. ReservationControllerTests, JeuxControllerTests).

    public AuthControllerTests(APIWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
    {
        var request = new LoginRequest
        {
            Email = "user1@nivo.fr",
            Password = "pasteque"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/login", request);
        var content = await response.Content.ReadAsStringAsync();

        Assert.Fail($"Status: {response.StatusCode}, Content: {content}");
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
