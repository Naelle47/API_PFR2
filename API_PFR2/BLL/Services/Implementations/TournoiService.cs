using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using API_PFR2.Domain.Exceptions;
namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides business logic operations related to <see cref="Tournoi"/> entities.
/// </summary>
/// <remarks>
/// This service coordinates tournament operations, including creation and cancellation.
/// When a tournament is created, all conflicting reservations are cancelled and affected
/// users are notified by email. When a tournament is cancelled, all registrations are deleted.
/// </remarks>
public class TournoiService : ITournoiService
{
    private readonly ITournoiRepository _tournoiRepository;
    private readonly IReservationService _reservationService;
    private readonly IInscriptionTournoiService _inscriptionTournoiService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TournoiService"/> class.
    /// </summary>
    /// <param name="tournoiRepository">Repository used to access tournament data.</param>
    /// <param name="reservationService">Service used to manage reservation cancellations.</param>
    /// <param name="inscriptionTournoiService">Service used to manage tournament registrations.</param>
    public TournoiService(
        ITournoiRepository tournoiRepository,
        IReservationService reservationService,
        IInscriptionTournoiService inscriptionTournoiService)
    {
        _tournoiRepository = tournoiRepository;
        _reservationService = reservationService;
        _inscriptionTournoiService = inscriptionTournoiService;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Tournoi>> GetAllAsync()
    {
        return await _tournoiRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task<Tournoi?> GetByIdAsync(int id)
    {
        return await _tournoiRepository.GetByIdAsync(id);
    }

    /// <inheritdoc/>
    public async Task<int> CreateAsync(Tournoi tournoi)
    {
        // Annulation des réservations conflictuelles et notification des utilisateurs
        await _reservationService.CancelReservationsForTournamentAsync(
            tournoi.jeuId, tournoi.dateDebut);

        return await _tournoiRepository.AddAsync(tournoi);
    }

    /// <inheritdoc/>
    public async Task CancelAsync(int id)
    {
        var tournoi = await _tournoiRepository.GetByIdAsync(id);
        if (tournoi == null)
            throw new NotFoundEntityException(nameof(Tournoi), id);

        // Suppression des inscriptions au tournoi
        await _inscriptionTournoiService.CancelByTournoiIdAsync(id);

        // Suppression du tournoi
        await _tournoiRepository.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Tournoi tournoi)
    {
        var current = await _tournoiRepository.GetByIdAsync(tournoi.id);
        if (current == null)
            throw new NotFoundEntityException(nameof(Tournoi), tournoi.id);

        int inscriptionCount = await _tournoiRepository.CountInscriptionsAsync(tournoi.id);
        if (inscriptionCount >= tournoi.capacite)
            throw new ConflictException(
                $"Cannot update tournament capacity — {inscriptionCount} users are already registered.");

        await _tournoiRepository.UpdateAsync(tournoi);
    }
}