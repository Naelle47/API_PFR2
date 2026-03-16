using API_PFR2.Domain.Entities;
namespace API_PFR2.BLL.Services.Interfaces;

/// <summary>
/// Provides business logic operations related to reservations.
/// </summary>
/// <remarks>
/// This service acts as an intermediary between controllers and the data access layer,
/// enforcing business rules such as preventing duplicate reservations and handling
/// automatic cancellations when required (e.g., when a tournament takes priority).
/// </remarks>
public interface IReservationService
{
    /// <summary>
    /// Creates a new reservation for a game.
    /// </summary>
    /// <param name="reservation">The reservation to create.</param>
    /// <returns>A task representing the asynchronous operation, containing the identifier of the newly created reservation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the game is already reserved for the specified time.
    /// </exception>
    Task<int> CreateReservationAsync(Reservation reservation);

    /// <summary>
    /// Determines whether a game is already reserved at a specific date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date to check.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing <c>true</c> if a reservation
    /// exists for the specified game and date; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> IsGameReservedAsync(int jeuId, DateTime date);

    /// <summary>
    /// Retrieves reservations for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date used to filter reservations.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a collection of reservations
    /// matching the specified game and date.
    /// </returns>
    Task<IEnumerable<Reservation>> GetReservationsByGameAndDateAsync(int jeuId, DateTime date);

    /// <summary>
    /// Cancels reservations for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date of the reservations to cancel.</param>
    /// <returns>A task representing the asynchronous operation, containing the number of reservations cancelled.</returns>
    Task<int> CancelReservationAsync(int jeuId, DateTime date);

    /// <summary>
    /// Cancels reservations that conflict with a tournament schedule.
    /// </summary>
    /// <remarks>
    /// According to the business rules, tournaments take priority over regular
    /// reservations. This method removes conflicting reservations and notifies
    /// the affected users.
    /// </remarks>
    /// <param name="jeuId">The identifier of the game used in the tournament.</param>
    /// <param name="date">The tournament date.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CancelReservationsForTournamentAsync(int jeuId, DateTime date);
}