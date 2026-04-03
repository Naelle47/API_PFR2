using System.Net;
using System.Net.Http.Json;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using API_PFR2_TestsIntegration.Fixtures;
using Xunit;

namespace API_PFR2_TestsIntegration
{
    public class ReservationControllerTests : AbstractIntegrationTest
    {
        public ReservationControllerTests(APIWebApplicationFactory fixture) : base(fixture)
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

        private static CreateReservationRequest BuildValidRequest(int offsetDays = 2)
        {
            var start = DateTime.UtcNow.Date.AddDays(offsetDays).AddHours(10);
            return new CreateReservationRequest
            {
                UtilisateurId = 1,
                JeuId = 1,
                DateDebut = start,
                DateFin = start.AddHours(2)
            };
        }

        // ─── Cas nominaux ──────────────────────────────────────────────────────

        [Fact]
        public async Task CreateReservation_ShouldReturnCreated_WhenDataIsValid()
        {
            await AuthenticateAsync();

            var response = await _client.PostAsJsonAsync("/api/reservation", BuildValidRequest(offsetDays: 5));
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var reservationId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(reservationId > 0);
        }

        [Fact]
        public async Task GetReservations_ShouldReturnCreatedReservation_WhenItExists()
        {
            await AuthenticateAsync();

            var request = BuildValidRequest(offsetDays: 6);
            var createResponse = await _client.PostAsJsonAsync("/api/reservation", request);
            createResponse.EnsureSuccessStatusCode();

            var date = request.DateDebut.Date;
            var response = await _client.GetAsync($"/api/reservation?jeuId={request.JeuId}&date={date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var reservations = await response.Content.ReadFromJsonAsync<ReservationResponse[]>();
            Assert.NotNull(reservations);
            Assert.NotEmpty(reservations);
            Assert.All(reservations, r => Assert.Equal(request.JeuId, r.JeuId));
        }

        [Fact]
        public async Task CancelReservation_ShouldDeleteReservations_AndLeaveNoneForThatDate()
        {
            await AuthenticateAsync();

            var request = BuildValidRequest(offsetDays: 7);
            var createResponse = await _client.PostAsJsonAsync("/api/reservation", request);
            createResponse.EnsureSuccessStatusCode();

            var date = request.DateDebut.Date;
            var deleteResponse = await _client.DeleteAsync($"/api/reservation?jeuId={request.JeuId}&date={date:yyyy-MM-dd}");
            deleteResponse.EnsureSuccessStatusCode();

            var cancelledCount = await deleteResponse.Content.ReadFromJsonAsync<int>();
            Assert.True(cancelledCount > 0, "Au moins une réservation devrait être annulée.");

            var verifyResponse = await _client.GetAsync($"/api/reservation?jeuId={request.JeuId}&date={date:yyyy-MM-dd}");
            var remaining = await verifyResponse.Content.ReadFromJsonAsync<ReservationResponse[]>();
            Assert.Empty(remaining);
        }

        // ─── Cas d'erreur ──────────────────────────────────────────────────────

        [Fact]
        public async Task CreateReservation_ShouldReturnUnauthorized_WhenNotAuthenticated()
        {
            var response = await _client.PostAsJsonAsync("/api/reservation", BuildValidRequest());

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnBadRequest_WhenDateFinBeforeDateDebut()
        {
            await AuthenticateAsync();

            var start = DateTime.UtcNow.Date.AddDays(10).AddHours(10);
            var request = new CreateReservationRequest
            {
                UtilisateurId = 1,
                JeuId = 1,
                DateDebut = start,
                DateFin = start.AddHours(-1) // incohérent
            };

            var response = await _client.PostAsJsonAsync("/api/reservation", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetReservations_ShouldReturnEmpty_WhenNoneExistForThatDate()
        {
            await AuthenticateAsync();

            // Date très lointaine pour laquelle aucune réservation n'existe
            var date = DateTime.UtcNow.Date.AddYears(10);
            var response = await _client.GetAsync($"/api/reservation?jeuId=1&date={date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var reservations = await response.Content.ReadFromJsonAsync<ReservationResponse[]>();
            Assert.Empty(reservations);
        }
    }
}