using API_PFR2.Domain.Enums;

namespace API_PFR2.Domain.Entities;

// Classe de liaison entre les entités Utilisateur et Tournoi, représentant l'inscription d'un utilisateur à un tournoi
// Elle représente l'inscription d'un utilisateur à un tournoi, avec des détails tels que l'utilisateur inscrit, le tournoi concerné, la date d'inscription, etc.

/// <summary>
/// Represents a user's registration for a tournament, including details such as the registration date, status, and
/// references to the user and tournament entities.
/// </summary>
/// <remarks>This class serves as a link between the user and tournament entities, capturing the state and details
/// of a user's registration. The registration status is defined by the StatutInscription enumeration, which includes
/// values such as 'EnAttente', 'Validee', and 'Refusee'.</remarks>
public class InscriptionTournoi
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime dateInscription { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public StatutInscription statut { get; set; } // Statut provenant de l'énumération StatutInscription avec les valeurs : "EnAttente", "Validee", "Refusee"

    // Clés étrangères pour les relations avec les entités Utilisateur et Tournoi
    /// <summary>
    /// 
    /// </summary>
    public int utilisateurId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tournoiId { get; set; }
}
