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
    public int CreateReservation(Reservation reservation)
    {
        bool exists = _reservationRepository
            .ExistsForGameAtDate(reservation.jeuId, reservation.dateDebut);

        if (exists)
        {
            throw new Exception("This game is already reserved for the selected date.");
        }

        return _reservationRepository.Add(reservation);
    }

    /// <inheritdoc/>
    public bool IsGameReserved(int jeuId, DateTime date)
    {
        return _reservationRepository.ExistsForGameAtDate(jeuId, date);
    }

    /// <inheritdoc/>
    public IEnumerable<Reservation> GetReservationsByGameAndDate(int jeuId, DateTime date)
    {
        return _reservationRepository.GetByGameAndDate(jeuId, date);
    }

    /// <inheritdoc/>
    public int CancelReservation(int jeuId, DateTime date)
    {
        return _reservationRepository.DeleteByGameAndDate(jeuId, date);
    }

    /// <inheritdoc/>
    public void CancelReservationsForTournament(int jeuId, DateTime date)
    {
        var reservations = _reservationRepository.GetByGameAndDate(jeuId, date);

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

        _reservationRepository.DeleteByGameAndDate(jeuId, date);
    }
}