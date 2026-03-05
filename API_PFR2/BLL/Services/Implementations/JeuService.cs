using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;

namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides business logic operations related to <see cref="Jeu"/> entities.
/// </summary>
/// <remarks>
/// This service acts as an intermediary between the controllers and the data access layer,
/// using <see cref="IJeuRepository"/> to retrieve game data.
/// </remarks>
public class JeuService : IJeuService
{
    private readonly IJeuRepository _jeuRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="JeuService"/> class.
    /// </summary>
    /// <param name="jeuRepository">
    /// Repository used to access game data.
    /// </param>
    public JeuService(IJeuRepository jeuRepository)
    {
        _jeuRepository = jeuRepository;
    }

    /// <summary>
    /// Retrieves the catalogue of games available in the system.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Jeu"/> representing all available games.
    /// </returns>
    public IEnumerable<Jeu> GetCatalogue()
    {
        return _jeuRepository.GetAll();
    }

    /// <summary>
    /// Retrieves a specific game using its identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the game.
    /// </param>
    /// <returns>
    /// The <see cref="Jeu"/> if it exists; otherwise <c>null</c>.
    /// </returns>
    public Jeu? GetJeuById(int id)
    {
        if (!_jeuRepository.Exists(id))
            return null;

        return _jeuRepository.GetById(id);
    }

    /// <summary>
    /// Checks if a game exists in the database by delegating to the repository.
    /// </summary>
    /// <param name="id">The identifier of the game to check.</param>
    /// <returns>
    /// <c>true</c> if the game exists; <c>false</c> otherwise.
    /// </returns>
    public bool Exists(int id) => _jeuRepository.Exists(id);
}
