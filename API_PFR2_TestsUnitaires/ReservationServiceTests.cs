using API_PFR2.BLL.Services.Implementations;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using API_PFR2.Domain.Exceptions;
using Moq;

namespace API_PFR2_TestsUnitaires;

public class ReservationServiceTests
{
    private readonly Mock<IReservationRepository> _reservationRepositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly ReservationService _reservationService;

    public ReservationServiceTests()
    {
        _reservationRepositoryMock = new Mock<IReservationRepository>();
        _emailServiceMock = new Mock<IEmailService>();
        _reservationService = new ReservationService(
            _reservationRepositoryMock.Object,
            _emailServiceMock.Object
        );
    }

    [Fact]
    public async Task CreateReservationAsync_ShouldThrow_WhenGameAlreadyReserved()
    {
        // Arrange
        var reservation = new Reservation
        {
            jeuId = 1,
            dateDebut = DateTime.Today,
            dateFin = DateTime.Today.AddHours(2),
            utilisateurId = 1
        };

        _reservationRepositoryMock
            .Setup(r => r.ExistsForGameAtDateAsync(reservation.jeuId, reservation.dateDebut))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(
            () => _reservationService.CreateReservationAsync(reservation)
        );
    }

    [Fact]
    public async Task CreateReservationAsync_ShouldReturnId_WhenGameIsAvailable()
    {
        // Arrange
        var reservation = new Reservation
        {
            jeuId = 1,
            dateDebut = DateTime.Today,
            dateFin = DateTime.Today.AddHours(2),
            utilisateurId = 1
        };

        _reservationRepositoryMock
            .Setup(r => r.ExistsForGameAtDateAsync(reservation.jeuId, reservation.dateDebut))
            .ReturnsAsync(false);

        _reservationRepositoryMock
            .Setup(r => r.AddAsync(reservation))
            .ReturnsAsync(1);

        // Act
        int result = await _reservationService.CreateReservationAsync(reservation);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task CancelReservationsForTournamentAsync_ShouldSendEmail_WhenUsersHaveReservations()
    {
        // Arrange — EmailUtilisateur directement dans la réservation
        var reservations = new List<Reservation>
        {
            new Reservation
            {
                id = 1,
                jeuId = 1,
                utilisateurId = 1,
                dateDebut = DateTime.Today,
                dateFin = DateTime.Today.AddHours(2),
                EmailUtilisateur = "user1@nivo.fr"
            }
        };

        _reservationRepositoryMock
            .Setup(r => r.GetByGameAndDateAsync(1, DateTime.Today))
            .ReturnsAsync(reservations);

        // Act
        await _reservationService.CancelReservationsForTournamentAsync(1, DateTime.Today);

        // Assert — l'email doit être envoyé à user1@nivo.fr
        _emailServiceMock.Verify(
            e => e.Send("user1@nivo.fr", It.IsAny<string>(), It.IsAny<string>()),
            Times.Once
        );

        // Assert — la suppression doit être appelée
        _reservationRepositoryMock.Verify(
            r => r.DeleteByGameAndDateAsync(1, DateTime.Today),
            Times.Once
        );
    }

    [Fact]
    public async Task IsGameReservedAsync_ShouldReturnTrue_WhenReservationExists()
    {
        // Arrange
        _reservationRepositoryMock
            .Setup(r => r.ExistsForGameAtDateAsync(1, DateTime.Today))
            .ReturnsAsync(true);

        // Act
        bool result = await _reservationService.IsGameReservedAsync(1, DateTime.Today);

        // Assert
        Assert.True(result);
    }
}