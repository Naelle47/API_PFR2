using API_PFR2.Domain.Enums;

namespace API_PFR2.Domain.Entities;

// Classe représentant un utilisateur de l'application (utilisateur/joueur ou administrateur).
/// <summary>
/// Represents a user of the application, which can be either a player or an administrator.
/// </summary>
/// <remarks>This class encapsulates user-related information, including a unique identifier, email, password hash
/// for secure storage, and the user's role as defined by the RoleUtilisateur enumeration.</remarks>
public class Utilisateur
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// Gets or sets the email address associated with the user.
    /// </summary>
    /// <remarks>The email address must be in a valid format. It is used for communication and account
    /// verification purposes.</remarks>
    public required string email { get; set; }
    /// <summary>
    /// Gets or sets the hashed representation of the user's password for secure storage.
    /// </summary>
    /// <remarks>The password hash is generated using a secure hashing algorithm to protect the user's
    /// password from unauthorized access. It is essential to ensure that the password is hashed before storage to
    /// enhance security.</remarks>
    public required string passwordHash { get; set; } // Stockage sécurisé du mot de passe sous forme de hash
    /// <summary>
    /// Gets or sets the user role, which determines the access level for the user.
    /// </summary>
    /// <remarks>The value of this property is defined by the RoleUtilisateur enumeration, which specifies the
    /// available roles such as 'Utilisateur' and 'Administrateur'. Assigning a role controls the permissions and
    /// capabilities granted to the user within the system.</remarks>
    public RoleUtilisateur role { get; set; } // Role provenant de l'énumération Role avec les valeurs : "Utilisateur", "Administrateur"
}
