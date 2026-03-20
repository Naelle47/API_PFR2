using API_PFR2.Domain.Entities;
namespace API_PFR2.BLL.Services.Interfaces;

/// <summary>
/// Defines business operations related to <see cref="Tournoi"/> entities.
/// </summary>
public interface ITournoiService
{
    /// <summary>
    /// Retrieves all tournaments.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing a collection of all tournaments.</returns>
    Task<IEnumerable<Tournoi>> GetAllAsync();

    /// <summary>
    /// Retrieves a tournament by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament.</param>
    /// <returns>A task representing the asynchronous operation, containing the corresponding <see cref="Tournoi"/> if found; otherwise <c>null</c>.</returns>
    Task<Tournoi?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new tournament.
    /// </summary>
    /// <param name="tournoi">The tournament to create.</param>
    /// <returns>A task representing the asynchronous operation, containing the identifier of the created tournament.</returns>
    Task<int> CreateAsync(Tournoi tournoi);

    /// <summary>
    /// Cancels a tournament and notifies affected users.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament to cancel.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CancelAsync(int id);

    /// <summary>
    /// Updates a tournament if its capacity has not been reached.
    /// </summary>
    /// <param name="tournoi">The tournament with updated values.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the tournament is not found or when it is at full capacity.
    /// </exception>
    Task UpdateAsync(Tournoi tournoi);
}