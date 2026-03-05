using API_PFR2.Domain.Entities;

namespace API_PFR2.BLL.Services.Interfaces;
/// <summary>
/// Defines business operations related to <see cref="Jeu"/> entities.
/// </summary>
public interface IJeuService
{
    /// <summary>
    /// Retrieves the catalogue of all available games.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Jeu"/> representing the game catalogue.
    /// </returns>
    IEnumerable<Jeu> GetCatalogue();

    /// <summary>
    /// Retrieves a game by its identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the game.
    /// </param>
    /// <returns>
    /// The corresponding <see cref="Jeu"/> if found; otherwise <c>null</c>.
    /// </returns>
    Jeu? GetJeuById(int id);

    /// <summary>
    /// Checks if a game exists in the database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the game to check.</param>
    /// <returns>
    /// <c>true</c> if the game exists; <c>false</c> otherwise.
    /// </returns>
    bool Exists(int id);
}
