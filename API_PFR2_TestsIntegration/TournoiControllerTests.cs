using System.Net;
using System.Net.Http.Json;
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

        // ─── Helper ────────────────────────────────────────────────────────────

        private async Task AuthenticateAsync(string email = "user1@nivo.fr", string password = "pasteque")
        {
            var loginRequest = new LoginRequest { Email = email, Password = password };

            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);
            loginResponse.EnsureSuccessStatusCode();

            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult!.Token);
        }

        private static CreateTournoiRequest BuildValidRequest(int offsetDays = 5) =>
            new CreateTournoiRequest
            {
                Nom = "Tournoi Test",
                JeuId = 1,
                DateDebut = DateTime.UtcNow.Date.AddDays(offsetDays).AddHours(10),
                DateFin = DateTime.UtcNow.Date.AddDays(offsetDays).AddHours(12),
                Capacite = 4
            };

        // ─── Cas nominaux ──────────────────────────────────────────────────────

        [Fact]
        public async Task CreateTournoi_ShouldReturnCreated_WhenDataIsValid()
        {
            await AuthenticateAsync();

            var response = await _client.PostAsJsonAsync("/api/tournoi", BuildValidRequest());
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var tournoiId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(tournoiId > 0);
        }

        [Fact]
        public async Task CreateTournoi_ShouldCancelExistingReservations_OnSameDateAndJeu()
        {
            await AuthenticateAsync();

            // Crée d'abord une réservation sur le même créneau
            var debut = DateTime.UtcNow.Date.AddDays(8).AddHours(10);
            var reservation = new CreateReservationRequest
            {
                UtilisateurId = 1,
                JeuId = 1,
                DateDebut = debut,
                DateFin = debut.AddHours(2)
            };
            var reservationResponse = await _client.PostAsJsonAsync("/api/reservation", reservation);
            reservationResponse.EnsureSuccessStatusCode();

            // Crée le tournoi sur le même créneau
            var tournoiRequest = new CreateTournoiRequest
            {
                Nom = "Tournoi Annule Resa",
                JeuId = 1,
                DateDebut = debut,
                DateFin = debut.AddHours(2),
                Capacite = 4
            };
            var tournoiResponse = await _client.PostAsJsonAsync("/api/tournoi", tournoiRequest);
            tournoiResponse.EnsureSuccessStatusCode();

            // Vérifie que les réservations ont bien été annulées
            var verifyResponse = await _client.GetAsync($"/api/reservation?jeuId=1&date={debut.Date:yyyy-MM-dd}");
            var remaining = await verifyResponse.Content.ReadFromJsonAsync<ReservationResponse[]>();
            Assert.Empty(remaining);
        }

        [Fact]
        public async Task GetAllTournois_ShouldReturnCreatedTournoi_WhenItExists()
        {
            await AuthenticateAsync();

            // Crée un tournoi pour garantir qu'au moins un existe
            var createResponse = await _client.PostAsJsonAsync("/api/tournoi", BuildValidRequest(offsetDays: 9));
            createResponse.EnsureSuccessStatusCode();

            var response = await _client.GetAsync("/api/tournoi");
            response.EnsureSuccessStatusCode();

            var tournois = await response.Content.ReadFromJsonAsync<TournoiResponse[]>();
            Assert.NotNull(tournois);
            Assert.NotEmpty(tournois);
        }

        [Fact]
        public async Task CancelTournoi_ShouldReturnNoContent_WhenTournoiExists()
        {
            await AuthenticateAsync();

            var createResponse = await _client.PostAsJsonAsync("/api/tournoi", BuildValidRequest(offsetDays: 12));
            createResponse.EnsureSuccessStatusCode();
            var tournoiId = await createResponse.Content.ReadFromJsonAsync<int>();

            var deleteResponse = await _client.DeleteAsync($"/api/tournoi/{tournoiId}");

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        // ─── Cas d'erreur ──────────────────────────────────────────────────────

        [Fact]
        public async Task CreateTournoi_ShouldReturnUnauthorized_WhenNotAuthenticated()
        {
            var response = await _client.PostAsJsonAsync("/api/tournoi", BuildValidRequest());

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CreateTournoi_ShouldReturnBadRequest_WhenDateFinBeforeDateDebut()
        {
            await AuthenticateAsync();

            var debut = DateTime.UtcNow.Date.AddDays(15).AddHours(10);
            var request = new CreateTournoiRequest
            {
                Nom = "Tournoi Invalide",
                JeuId = 1,
                DateDebut = debut,
                DateFin = debut.AddHours(-1), // incohérent
                Capacite = 4
            };

            var response = await _client.PostAsJsonAsync("/api/tournoi", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CancelTournoi_ShouldReturnNotFound_WhenTournoiDoesNotExist()
        {
            await AuthenticateAsync();

            var response = await _client.DeleteAsync("/api/tournoi/999999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}