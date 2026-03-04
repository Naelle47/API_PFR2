using PFR2_API.Domain.Enums;

namespace PFR2_API.Domain.Entities;

// Classe de liaison entre les entités Utilisateur et Tournoi, représentant l'inscription d'un utilisateur à un tournoi
// Elle représente l'inscription d'un utilisateur à un tournoi, avec des détails tels que l'utilisateur inscrit, le tournoi concerné, la date d'inscription, etc.

public class InscriptionTournoi 
{
    public int id { get; set; }
    public DateTime dateInscription { get; set; }
    public StatutInscription statut { get; set; } // Statut provenant de l'énumération StatutInscription avec les valeurs : "EnAttente", "Validee", "Refusee"

    // Clés étrangères pour les relations avec les entités Utilisateur et Tournoi
    public int utilisateurId { get; set; }
    public int tournoiId { get; set; }
}
