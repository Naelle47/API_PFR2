using API_PFR2.Domain.Entities;
namespace API_PFR2.DAL.Interfaces;

/// <summary>
/// Defines data access operations for <see cref="InscriptionTournoi"/> entities.
/// </summary>
public interface IInscriptionTournoiRepository
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
    /// Adds a new registration to the data store.
    /// </summary>
    /// <param name="inscription">The registration to add.</param>
    /// <returns>A task representing the asynchronous operation, containing the identifier of the created registration.</returns>
    Task<int> AddAsync(InscriptionTournoi inscription);

    /// <summary>
    /// Deletes all registrations associated with a specific tournament.
    /// </summary>
    /// <param name="tournoiId">The identifier of the tournament.</param>
    /// <returns>A task representing the asynchronous operation, containing the number of registrations deleted.</returns>
    Task<int> DeleteByTournoiIdAsync(int tournoiId);

    /// <summary>
    /// Determines whether a user is already registered for a specific tournament.
    /// </summary>
    /// <param name="utilisateurId">The identifier of the user.</param>
    /// <param name="tournoiId">The identifier of the tournament.</param>
    /// <returns>A task representing the asynchronous operation, containing <c>true</c> if the registration exists; otherwise <c>false</c>.</returns>
    Task<bool> ExistsAsync(int utilisateurId, int tournoiId);
}