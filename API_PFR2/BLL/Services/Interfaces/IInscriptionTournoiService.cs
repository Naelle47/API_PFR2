using API_PFR2.Domain.Entities;
namespace API_PFR2.BLL.Services.Interfaces;

/// <summary>
/// Defines business operations related to <see cref="InscriptionTournoi"/> entities.
/// </summary>
public interface IInscriptionTournoiService
{
    /// <summary>
    /// Retrieves all registrations for a specific tournament.
    /// </summary>
    /// <param name="tournoiId">The identifier of the tournament.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of registrations.</returns>
    Task<IEnumerable<InscriptionTournoi>> GetByTournoiIdAsync(int tournoiId);

    /// <summary>
    /// Retrieves all registrations for a specific user.
    /// </summary>
    /// <param name="utilisateurId">The identifier of the user.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of registrations.</returns>
    Task<IEnumerable<InscriptionTournoi>> GetByUtilisateurIdAsync(int utilisateurId);

    /// <summary>
    /// Registers a user for a tournament.
    /// </summary>
    /// <param name="utilisateurId">The identifier of the user.</param>
    /// <param name="tournoiId">The identifier of the tournament.</param>
    /// <returns>A task representing the asynchronous operation, containing the identifier of the created registration.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the user is already registered for the tournament or when the tournament is at full capacity.
    /// </exception>
    Task<int> RegisterAsync(int utilisateurId, int tournoiId);

    /// <summary>
    /// Cancels all registrations for a specific tournament.
    /// </summary>
    /// <param name="tournoiId">The identifier of the tournament.</param>
    /// <returns>A task representing the asynchronous operation, containing the number of registrations cancelled.</returns>
    Task<int> CancelByTournoiIdAsync(int tournoiId);
}