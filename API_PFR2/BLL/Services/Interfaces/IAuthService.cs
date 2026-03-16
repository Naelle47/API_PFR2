
namespace API_PFR2.BLL.Services.Interfaces;

/// <summary>
/// Defines authentication-related operations for the application.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user using their email and password, and returns a JWT token if successful.
    /// </summary>
    /// <param name="email">The email address of the user attempting to log in.</param>
    /// <param name="password">The plain text password provided by the user.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a JWT token string
    /// if authentication succeeded; otherwise <c>null</c>.
    /// </returns>
    Task<string?> LoginAsync(string email, string password);
}
