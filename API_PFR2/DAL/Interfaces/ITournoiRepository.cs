using API_PFR2.Domain.Entities;
namespace API_PFR2.DAL.Interfaces;

/// <summary>
/// Defines data access operations for <see cref="Tournoi"/> entities.
/// </summary>
public interface ITournoiRepository
{
    /// <summary>
    /// Retrieves all tournaments from the data source.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing a collection of all tournaments.</returns>
    Task<IEnumerable<Tournoi>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific tournament by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament.</param>
    /// <returns>A task representing the asynchronous operation, containing the corresponding <see cref="Tournoi"/> if found; otherwise <c>null</c>.</returns>
    Task<Tournoi?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new tournament to the data store.
    /// </summary>
    /// <param name="tournoi">The tournament to add.</param>
    /// <returns>A task representing the asynchronous operation, containing the identifier of the created tournament.</returns>
    Task<int> AddAsync(Tournoi tournoi);

    /// <summary>
    /// Deletes a tournament by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament to delete.</param>
    /// <returns>A task representing the asynchronous operation, containing the number of rows affected.</returns>
    Task<int> DeleteAsync(int id);

    /// <summary>
    /// Determines whether a tournament exists for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date to check.</param>
    /// <returns>A task representing the asynchronous operation, containing <c>true</c> if a tournament exists; otherwise <c>false</c>.</returns>
    Task<bool> ExistsForGameAtDateAsync(int jeuId, DateTime date);
}