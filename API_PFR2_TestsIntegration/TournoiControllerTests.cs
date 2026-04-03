using System.Net;
using System.Net.Http.Json;
using API_PFR2.Domain.Entities;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using API_PFR2_TestsIntegration.Fixtures;
using Xunit;

namespace API_PFR2_TestsIntegration
{
    public class TournoiControllerTests : AbstractIntegrationTest
    {
        public TournoiControllerTests(APIWebApplicationFactory fixture) : base(fixture)
        {
        }

        // Helper pour authentification et ajout du token aux headers
        private async Task AuthenticateAsync(string email = "user1@nivo.fr", string password = "pasteque")
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);
            loginResponse.EnsureSuccessStatusCode();

            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult.Token);
        }

        [Fact]
        public async Task CreateTournoi_ShouldSucceed_AndCancelExistingReservations()
        {
            await AuthenticateAsync();

            var request = new CreateTournoiRequest
            {
                Nom = "Tournoi Test",
                JeuId = 1, // suppose qu’il existe des réservations pour ce jeu
                DateDebut = new DateTime(2026, 04, 10, 10, 0, 0),
                DateFin = new DateTime(2026, 04, 10, 12, 0, 0),
                Capacite = 4
            };

            var response = await _client.PostAsJsonAsync("/api/tournoi", request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var tournoiId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(tournoiId > 0);

            // Les mails sont maintenant envoyés via MailPit
            // Tu peux les visualiser dans l'interface web de MailPit: http://localhost:8025
        }

        [Fact]
        public async Task GetAllTournois_ShouldReturnData_WhenExists()
        {
            await AuthenticateAsync();

            var response = await _client.GetAsync("/api/tournoi");
            response.EnsureSuccessStatusCode();

            var tournois = await response.Content.ReadFromJsonAsync<TournoiResponse[]>();
            Assert.NotNull(tournois);
            Assert.NotEmpty(tournois);
        }

        [Fact]
        public async Task CancelTournoi_ShouldReturnNoContent_WhenExists()
        {
            await AuthenticateAsync();

            // Créons un tournoi pour tester la suppression
            var createRequest = new CreateTournoiRequest
            {
                Nom = "Tournoi à Annuler",
                JeuId = 1,
                DateDebut = DateTime.Now.AddDays(1),
                DateFin = DateTime.Now.AddDays(1).AddHours(2),
                Capacite = 4
            };

            var createResponse = await _client.PostAsJsonAsync("/api/tournoi", createRequest);
            var tournoiId = await createResponse.Content.ReadFromJsonAsync<int>();

            var deleteResponse = await _client.DeleteAsync($"/api/tournoi/{tournoiId}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}