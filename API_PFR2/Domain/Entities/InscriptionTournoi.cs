using API_PFR2.Domain.Enums;
namespace API_PFR2.Domain.Entities;

// Classe de liaison entre les entités Utilisateur et Tournoi, représentant l'inscription d'un utilisateur à un tournoi
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
    /// Gets or sets the unique identifier of the registration.
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user registered for the tournament.
    /// </summary>
    public DateTime dateInscription { get; set; }

    /// <summary>
    /// Gets or sets the current status of the registration, as defined by the <see cref="StatutInscription"/> enumeration.
    /// </summary>
    public StatutInscription statut { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who registered for the tournament.
    /// </summary>
    public int utilisateurId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the tournament the user registered for.
    /// </summary>
    public int tournoiId { get; set; }
}