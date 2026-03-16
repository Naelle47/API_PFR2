using API_PFR2.Domain.Entities;
namespace API_PFR2.DAL.Interfaces;
/// <summary>
/// Defines data access operations for <see cref="Utilisateur"/> entities.
/// </summary>
public interface IUtilisateurRepository
{
    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the corresponding
    /// <see cref="Utilisateur"/> if found; otherwise <c>null</c>.
    /// </returns>
    Task<Utilisateur?> GetByEmailAsync(string email);

    /// <summary>
    /// Determines whether a user with the specified email exists.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing <c>true</c>
    /// if the user exists; otherwise <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(string email);

}
