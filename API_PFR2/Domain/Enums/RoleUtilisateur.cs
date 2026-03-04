namespace PFR2_API.Domain.Enums;

// Enumération représentant les différents rôles
// Les rôles sont : Utilsateur/Joueur et Administrateur. Ces rôles déterminent les permissions et les actions que chaque utilisateur peut effectuer au sein de l’application.
public enum RoleUtilisateur
{

    Utilisateur = 0, // Rôle d'utilisateur/joueur, avec des permissions limitées principalement à la réservation de jeux et à la participation aux tournois.
    Admin = 1 // Rôle d'administrateur, avec des permissions étendues pour gérer les jeux, les utilisateurs, les réservations, etc.
}
