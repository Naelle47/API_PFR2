using API_PFR2.Domain.Entities;
namespace API_PFR2.DAL.Interfaces;

/// <summary>
/// Defines data access operations for <see cref="Jeu"/> entities.
/// </summary>
public interface IJeuRepository
{
    /// <summary>
    /// Retrieves all games from the data source.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation, containing a collection of all available games.
    /// </returns>
    Task<IEnumerable<Jeu>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific game by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the game.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the corresponding <see cref="Jeu"/> if found; otherwise <c>null</c>.
    /// </returns>
    Task<Jeu?> GetByIdAsync(int id);

    /// <summary>
    /// Determines whether a game with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the game.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing <c>true</c> if the game exists; otherwise <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(int id);
}