using API_PFR2.Domain.Entities;
namespace API_PFR2.DAL.Interfaces;

/// <summary>
/// Defines data access operations for managing reservations.
/// </summary>
/// <remarks>
/// This repository provides methods to create, retrieve, verify, and delete reservations
/// associated with games and users.
/// </remarks>
public interface IReservationRepository
{
    /// <summary>
    /// Adds a new reservation to the data store.
    /// </summary>
    /// <param name="reservation">The reservation to add.</param>
    /// <returns>A task representing the asynchronous operation, containing the identifier of the created reservation.</returns>
    Task<int> AddAsync(Reservation reservation);

    /// <summary>
    /// Determines whether a reservation already exists for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date to check for an existing reservation.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing <c>true</c> if a reservation
    /// exists for the specified game at the given date; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsForGameAtDateAsync(int jeuId, DateTime date);

    /// <summary>
    /// Retrieves all reservations for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date used to filter reservations.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of matching reservations.</returns>
    Task<IEnumerable<Reservation>> GetByGameAndDateAsync(int jeuId, DateTime date);

    /// <summary>
    /// Deletes reservations associated with a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date used to identify reservations to delete.</param>
    /// <returns>A task representing the asynchronous operation, containing the number of reservations deleted.</returns>
    Task<int> DeleteByGameAndDateAsync(int jeuId, DateTime date);
}