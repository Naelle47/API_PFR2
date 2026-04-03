using System.Net;
using System.Net.Http.Json;
using API_PFR2.Domain.Enums;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using API_PFR2_TestsIntegration.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace API_PFR2_TestsIntegration
{
    public class ReservationControllerTests : AbstractIntegrationTest
    {
        public ReservationControllerTests(APIWebApplicationFactory fixture) : base(fixture)
        {
        }

        // -----------------------------
        // Helper : Authentifie le client
        // -----------------------------
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

        // -----------------------------
        // Test : création réservation valide
        // -----------------------------
        [Fact]
        public async Task CreateReservation_ShouldWork_WhenValidData()
        {
            await AuthenticateAsync();

            var start = DateTime.UtcNow.Date.AddDays(2).AddHours(10);
            var request = new CreateReservationRequest
            {
                UtilisateurId = 1,
                JeuId = 1,
                DateDebut = start,
                DateFin = start.AddHours(2)
            };

            var response = await _client.PostAsJsonAsync("/api/reservation", request);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var reservationId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(reservationId > 0);
        }

        // -----------------------------
        // Test : lecture réservation existante
        // -----------------------------
        [Fact]
        public async Task GetReservations_ShouldReturnData_WhenExists()
        {
            await AuthenticateAsync();

            var jeuId = 1;
            var date = DateTime.UtcNow.Date.AddDays(1); // date du seed pour Catan

            var response = await _client.GetAsync($"/api/reservation?jeuId={jeuId}&date={date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var reservations = await response.Content.ReadFromJsonAsync<ReservationResponse[]>();
            Assert.NotNull(reservations);
            Assert.NotEmpty(reservations); // au moins la réservation seed
            Assert.All(reservations, r => Assert.Equal(jeuId, r.JeuId));
        }

        // -----------------------------
        // Test : annulation réservation existante
        // -----------------------------
        [Fact]
        public async Task CancelReservation_ShouldWork_WhenReservationsExist()
        {
            await AuthenticateAsync();

            // Crée une réservation temporaire pour garantir qu'il y a quelque chose à annuler
            var start = DateTime.UtcNow.Date.AddDays(3).AddHours(10);
            var createRequest = new CreateReservationRequest
            {
                UtilisateurId = 1,
                JeuId = 1,
                DateDebut = start,
                DateFin = start.AddHours(2)
            };
            var createResponse = await _client.PostAsJsonAsync("/api/reservation", createRequest);
            createResponse.EnsureSuccessStatusCode();

            var jeuId = 1;
            var date = start.Date;

            // Supprime la réservation
            var response = await _client.DeleteAsync($"/api/reservation?jeuId={jeuId}&date={date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var cancelledCount = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(cancelledCount > 0, "Au moins une réservation devrait être annulée");

            // Vérifie qu'elles ont bien été supprimées
            var verifyResponse = await _client.GetAsync($"/api/reservation?jeuId={jeuId}&date={date:yyyy-MM-dd}");
            var reservations = await verifyResponse.Content.ReadFromJsonAsync<ReservationResponse[]>();
            Assert.Empty(reservations);
        }
    }
}