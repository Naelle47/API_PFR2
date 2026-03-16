using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Implements business logic related to reservations.
/// </summary>
/// <remarks>
/// This service coordinates reservation operations and enforces
/// application rules such as preventing double bookings and
/// cancelling reservations when a tournament is scheduled.
/// </remarks>
public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReservationService"/> class.
    /// </summary>
    /// <param name="reservationRepository">The reservation repository.</param>
    /// <param name="emailService">The email service used to notify users.</param>
    public ReservationService(
        IReservationRepository reservationRepository,
        IEmailService emailService)
    {
        _reservationRepository = reservationRepository;
        _emailService = emailService;
    }

    /// <inheritdoc/>
    public async Task<int> CreateReservationAsync(Reservation reservation)
    {
        bool exists = await _reservationRepository
            .ExistsForGameAtDateAsync(reservation.jeuId, reservation.dateDebut);
        if (exists)
        {
            throw new InvalidOperationException("This game is already reserved for the selected date.");
        }
        return await _reservationRepository.AddAsync(reservation);
    }

    /// <inheritdoc/>
    public async Task<bool> IsGameReservedAsync(int jeuId, DateTime date)
    {
        return await _reservationRepository.ExistsForGameAtDateAsync(jeuId, date);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Reservation>> GetReservationsByGameAndDateAsync(int jeuId, DateTime date)
    {
        return await _reservationRepository.GetByGameAndDateAsync(jeuId, date);
    }

    /// <inheritdoc/>
    public async Task<int> CancelReservationAsync(int jeuId, DateTime date)
    {
        return await _reservationRepository.DeleteByGameAndDateAsync(jeuId, date);
    }

    /// <inheritdoc/>
    public async Task CancelReservationsForTournamentAsync(int jeuId, DateTime date)
    {
        var reservations = await _reservationRepository.GetByGameAndDateAsync(jeuId, date);
        foreach (var reservation in reservations)
        {
            if (reservation.utilisateur != null)
            {
                _emailService.Send(
                    reservation.utilisateur.email,
                    "Reservation Cancelled",
                    "Your reservation has been cancelled because a tournament has been scheduled for this game."
                );
            }
        }
        await _reservationRepository.DeleteByGameAndDateAsync(jeuId, date);
    }
}