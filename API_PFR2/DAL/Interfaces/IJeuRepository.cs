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
    /// A collection containing all available games.
    /// </returns>
    IEnumerable<Jeu> GetAll();

    /// <summary>
    /// Retrieves a specific game by its identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the game.
    /// </param>
    /// <returns>
    /// The corresponding <see cref="Jeu"/> if found; otherwise <c>null</c>.
    /// </returns>
    Jeu? GetById(int id);

    /// <summary>
    /// Determines whether a game with the specified identifier exists.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the game.
    /// </param>
    /// <returns>
    /// <c>true</c> if the game exists; otherwise <c>false</c>.
    /// </returns>
    bool Exists(int id);

}
