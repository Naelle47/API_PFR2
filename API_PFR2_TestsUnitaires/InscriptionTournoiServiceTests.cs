using API_PFR2.BLL.Services.Implementations;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using API_PFR2.Domain.Enums;
using Moq;
namespace API_PFR2_TestsUnitaires;

public class InscriptionTournoiServiceTests
{
    private readonly Mock<IInscriptionTournoiRepository> _inscriptionRepositoryMock;
    private readonly Mock<ITournoiRepository> _tournoiRepositoryMock;
    private readonly InscriptionTournoiService _inscriptionService;

    public InscriptionTournoiServiceTests()
    {
        _inscriptionRepositoryMock = new Mock<IInscriptionTournoiRepository>();
        _tournoiRepositoryMock = new Mock<ITournoiRepository>();
        _inscriptionService = new InscriptionTournoiService(
            _inscriptionRepositoryMock.Object,
            _tournoiRepositoryMock.Object
        );
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrow_WhenUserAlreadyRegistered()
    {
        // Arrange
        _inscriptionRepositoryMock
            .Setup(r => r.ExistsAsync(1, 1))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _inscriptionService.RegisterAsync(1, 1)
        );
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrow_WhenTournamentAtFullCapacity()
    {
        // Arrange
        var tournoi = new Tournoi { id = 1, capacite = 2, jeuId = 1, dateDebut = DateTime.Today, dateFin = DateTime.Today.AddDays(1) };
        _inscriptionRepositoryMock
            .Setup(r => r.ExistsAsync(1, 1))
            .ReturnsAsync(false);
        _tournoiRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(tournoi);
        _tournoiRepositoryMock
            .Setup(r => r.CountInscriptionsAsync(1))
            .ReturnsAsync(2);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _inscriptionService.RegisterAsync(1, 1)
        );
    }

    [Fact]
    public async Task RegisterAsync_ShouldReturnId_WhenRegistrationIsValid()
    {
        // Arrange
        var tournoi = new Tournoi { id = 1, capacite = 10, jeuId = 1, dateDebut = DateTime.Today, dateFin = DateTime.Today.AddDays(1) };
        _inscriptionRepositoryMock
            .Setup(r => r.ExistsAsync(1, 1))
            .ReturnsAsync(false);
        _tournoiRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(tournoi);
        _tournoiRepositoryMock
            .Setup(r => r.CountInscriptionsAsync(1))
            .ReturnsAsync(5);
        _inscriptionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<InscriptionTournoi>()))
            .ReturnsAsync(1);

        // Act
        int result = await _inscriptionService.RegisterAsync(1, 1);

        // Assert
        Assert.Equal(1, result);
    }
}