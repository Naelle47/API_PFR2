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
/// When a tournament is cancelled, all associated reservations are cancelled and affected users are notified.
/// </remarks>
public class TournoiService : ITournoiService
{
    private readonly ITournoiRepository _tournoiRepository;
    private readonly IReservationService _reservationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TournoiService"/> class.
    /// </summary>
    /// <param name="tournoiRepository">Repository used to access tournament data.</param>
    /// <param name="reservationService">Service used to manage reservation cancellations.</param>
    public TournoiService(
        ITournoiRepository tournoiRepository,
        IReservationService reservationService)
    {
        _tournoiRepository = tournoiRepository;
        _reservationService = reservationService;
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
        return await _tournoiRepository.AddAsync(tournoi);
    }

    /// <inheritdoc/>
    public async Task CancelAsync(int id)
    {
        var tournoi = await _tournoiRepository.GetByIdAsync(id);
        if (tournoi == null)
            throw new NotFoundEntityException(nameof(Tournoi), id);

        // Cancel reservations and notify users
        await _reservationService.CancelReservationsForTournamentAsync(tournoi.jeuId, tournoi.dateDebut);

        // Delete the tournament
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