namespace API_PFR2.Domain.Enums;

// Enumération représentant les différents rôles
// Les rôles sont : Utilsateur/Joueur et Administrateur. Ces rôles déterminent les permissions et les actions que chaque utilisateur peut effectuer au sein de l’application.

/// <summary>
/// Specifies the user roles within the application, determining the permissions and actions available to each user.
/// </summary>
/// <remarks>The available roles are Utilisateur, representing a user or player with limited permissions such as
/// game reservations and tournament participation, and Admin, representing an administrator with extended permissions
/// to manage games, users, reservations, and other administrative tasks. The assigned role affects the level of access
/// and functionality provided to the user throughout the application.</remarks>
public enum RoleUtilisateur
{
    /// <summary>
    /// Represents a standard user role with basic access permissions.
    /// </summary>
    Utilisateur = 0, 

    /// <summary>
    /// Represents the administrator role, which grants extended permissions to manage games, users, reservations, and other system resources.
    /// </summary>
    Admin = 1 
}
