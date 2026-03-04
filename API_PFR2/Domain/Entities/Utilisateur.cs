using PFR2_API.Domain.Enums;

namespace PFR2_API.Domain.Entities;

// Classe représentant un utilisateur de l'application (utilisateur/joueur ou administrateur).
public class Utilisateur
{
    public int id { get; set; }
    public string email { get; set; }
    public required string passwordHash { get; set; } // Stockage sécurisé du mot de passe sous forme de hash
    public RoleUtilisateur role { get; set; } // Role provenant de l'énumération Role avec les valeurs : "Utilisateur", "Administrateur"
}
