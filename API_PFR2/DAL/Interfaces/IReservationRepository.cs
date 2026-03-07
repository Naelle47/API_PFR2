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
    /// <returns>The identifier of the created reservation.</returns>
    int Add(Reservation reservation);

    /// <summary>
    /// Determines whether a reservation already exists for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date to check for an existing reservation.</param>
    /// <returns>
    /// <c>true</c> if a reservation exists for the specified game at the given date; otherwise, <c>false</c>.
    /// </returns>
    bool ExistsForGameAtDate(int jeuId, DateTime date);

    /// <summary>
    /// Retrieves all reservations for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date used to filter reservations.</param>
    /// <returns>A collection of reservations matching the criteria.</returns>
    IEnumerable<Reservation> GetByGameAndDate(int jeuId, DateTime date);

    /// <summary>
    /// Deletes reservations associated with a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date used to identify reservations to delete.</param>
    /// <returns>The number of reservations deleted.</returns>
    int DeleteByGameAndDate(int jeuId, DateTime date);
}